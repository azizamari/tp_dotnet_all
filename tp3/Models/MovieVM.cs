using System.ComponentModel.DataAnnotations;

namespace tp3.Models
{
    public class MovieVM
    {
        public Movie movie { get; set; }
        
        [Display(Name = "Image")]
        public IFormFile photo { get; set; }
    }
}
