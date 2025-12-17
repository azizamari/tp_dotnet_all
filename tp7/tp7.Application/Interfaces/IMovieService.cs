using tp7.Domain.Entities;

namespace tp7.Application.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetFavoriteMovies(int userId);
        IEnumerable<Movie> GetMoviesByGenre(int genreId);
        void AddReview(int movieId, string comment, double rating);
        double GetAverageRating(int movieId);
    }
}
