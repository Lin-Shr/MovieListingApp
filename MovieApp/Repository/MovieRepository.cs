using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Interfaces;

namespace MovieApp.Repository
{
    public class MovieRepository : IMovies
    {
        private readonly MovieAppContext _context;

        public MovieRepository(MovieAppContext context)
        {
            _context = context;
        }

        public bool AddMovies(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteMovies(int id)
        {
            var movie = _context.Movies.Find(id);
            movie.IsDeleted = true;
            _context.Movies.Update(movie);
            _context.SaveChanges();
            return true;
        }


        public Movie GetById(int Id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == Id && !x.IsDeleted);
            
            if (movie == null) return new Movie();

            movie.Reviews = _context.Reviews.Where(x => x.MovieId == movie.Id).ToList();
            return movie;
            /*.Include(m => m.Genres)
            .FirstOrDefault(m => m.Id == Id);*/
        }

/*        public ICollection<Movie> GetMovies()
        {
            return _context.Movies.OrderBy(m => m.Title).ToList();
        }*/
        public bool UpdateMovies(Movie movie)
        {
            
            _context.Movies.Update(movie);
            _context.SaveChanges();
            return true;
        }

        public List<Movie> GetMovies()
        {
           return _context.Movies.ToList();
        }

        public List<Genre> GetGenres(int id)
        {
            return _context.GenreMovies
            .Where(gm => gm.MovieId == id)
            .Select(gm => gm.Genres)
            .ToList();
        }

        public bool MovieExists(int movieId)
        {
            return _context.Movies.Any(p => p.Id == movieId);
        }

        public bool MovieExists(string name)
        {
            return _context.Movies.Any(p => p.Title == name );
        }

        public Movie GetByName(string name)
        {
            
          return _context.Movies.Where(p => p.Title == name && !p.IsDeleted).FirstOrDefault();
            
        }

        public decimal GetMovieRating(string name)
        {
            var review = _context.Reviews.Where(p => p.Movie.Title == name);
            if (review.Count() <= 0)
            {
                return 0;
            }
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

    }
}