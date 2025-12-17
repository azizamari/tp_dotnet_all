namespace tp7.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public double AverageRating { get; set; }
    }

    public class Review
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Comment { get; set; }
        public double Rating { get; set; }
    }
}
