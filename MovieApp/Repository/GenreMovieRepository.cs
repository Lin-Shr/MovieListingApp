using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Interfaces;

namespace MovieApp.Repository
{
    public class GenreMovieRepository : IGenreMovie
    {
        private readonly MovieAppContext _context;

        public GenreMovieRepository(MovieAppContext context)
        {
            _context = context;
        }
        public void Add(GenreMovie genreMovie)
        {
            _context.GenreMovies.Add(genreMovie);
            _context.SaveChanges();
        }
    }
}
