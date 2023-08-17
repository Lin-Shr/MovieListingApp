using Microsoft.NET.StringTools;
using MovieApp.Areas.Identity.Data;

namespace MovieApp.Interfaces
{
    public interface IGenre
    {
        bool AddGenre(Genre genre);
        bool UpdateGenre(Genre genre);
        bool DeleteGenre(int id);
        Genre GetById(int id);
        List<Genre> GetAll();
        ICollection<Genre> GetGenreOfMovie(int movieId);
        List<Movie> GetMovies(int id);
        bool GenreExists(int id);
    }
}
