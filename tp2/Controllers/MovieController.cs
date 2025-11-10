using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tp2.Models;

namespace tp2.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationdbContext _db;

        public MovieController(ApplicationdbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            var movies = _db.movies.Include(m => m.Genre).AsQueryable();

            switch (sortOrder)
            {
                case "name_desc":
                    movies = movies.OrderByDescending(m => m.Title);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;
            }

            int pageSize = 3;
            var list = movies.ToList(); // Simplified pagination for speed
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            _db.movies.Add(movie);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var movie = _db.movies.Find(id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Movie movie)
        {
            if (id != movie.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _db.Update(movie);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var movie = _db.movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _db.movies.Find(id);
            if (movie != null)
            {
                _db.movies.Remove(movie);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
