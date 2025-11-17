namespace tp3.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? ImageFile { get; set; }
        public DateTime? DateAjoutMovie { get; set; }
    }
}
