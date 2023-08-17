using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Repository;

namespace MovieApp.Interfaces
{
    public interface IMovies
    {
        /*public ICollection<Movie> GetMovies();*/
        bool AddMovies(Movie movie);
        bool UpdateMovies(Movie movie);
        Movie GetById(int Id);
        Movie GetByName(string name);
        bool DeleteMovies(int id);
        List<Movie> GetMovies();
        List<Genre> GetGenres(int id);
        bool MovieExists(int pokeId);
        bool MovieExists(string name);
        decimal GetMovieRating(string name);
    }
}
