using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp2.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public int GenreId { get; set; }
        
        [ForeignKey("GenreId")]
        public Genre? Genre { get; set; }
    }
}
