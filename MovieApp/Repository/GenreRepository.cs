using Microsoft.EntityFrameworkCore;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Interfaces;

namespace MovieApp.Repository
{
    public class GenreRepository : IGenre
    {
        private readonly MovieAppContext _context;
        public GenreRepository(MovieAppContext context)
        {
            _context = context;
        }

        public bool AddGenre(Genre genre)
        {
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return true;
           
        }


        public bool DeleteGenre(int id)
        {
            var genre = _context.Genres.Find(id);
            genre.IsDeleted = true;
            _context.Genres.Update(genre);
            _context.SaveChanges();
            return true;
        }

        public bool GenreExists(int id)
        {
            return _context.Genres.Any(x => x.Id == id);
        }

        public List<Genre> GetAll()
        {
            var data = _context.Genres.ToList();
            return data;
        }

        public Genre GetById(int id)
        {
            return _context.Genres.Find(id);
        }

        public ICollection<Genre> GetGenreOfMovie(int movieId)
        {
            return _context.GenreMovies.Where(m => m.MovieId == movieId).Select(g => g.Genres).ToList();
        }

        public List<Movie> GetMovies(int id)
        {
           return _context.GenreMovies
                .Where(m => m.GenreId == id)
                .Select(m => m.Movies)
                .ToList();
        }

        public bool UpdateGenre(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
            return true;
        }
    }
}
