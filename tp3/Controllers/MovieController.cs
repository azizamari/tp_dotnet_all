using Microsoft.AspNetCore.Mvc;
using tp3.Models;

namespace tp3.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(_context.Movies.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieVM model, IFormFile photo)
        {
            if (photo == null)
            {
                ViewBag.Errors = new List<string> { "File not uploaded" };
            }

            if (ModelState.IsValid && photo != null)
            {
                try
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var path = Path.Combine(uploadsFolder, photo.FileName);
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }

                    var movie = new Movie
                    {
                        Id = Guid.NewGuid(),
                        Name = model.movie.Name,
                        DateAjoutMovie = model.movie.DateAjoutMovie,
                        ImageFile = photo.FileName,
                    };

                    _context.Add(movie);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.Errors = new List<string> { ex.Message };
                }
            }
            else
            {
                ViewBag.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
            }

            return View(model);
        }
    }
}
