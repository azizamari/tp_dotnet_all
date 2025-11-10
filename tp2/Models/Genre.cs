using System.ComponentModel.DataAnnotations;

namespace tp2.Models
{
    public class Genre
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public ICollection<Movie>? Movies { get; set; }
    }
}
