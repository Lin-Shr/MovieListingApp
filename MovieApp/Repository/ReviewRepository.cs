using Microsoft.EntityFrameworkCore;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Interfaces;

namespace MovieApp.Repository
{
    public class ReviewRepository : IReviews
    {
        private readonly MovieAppContext _context;
        public ReviewRepository(MovieAppContext context)
        {
            _context = context;
        }

        public bool AddComment(Review Comment)
        {
            _context.Reviews.Add(Comment);
            _context.SaveChanges();
            return true;
        }
        public bool DeleteComment(int id)
        {
            var comment = _context.Reviews.Find(id);
            comment.IsDeleted = true;
            _context.Reviews.Update(comment);
            _context.SaveChanges();
            return true;
        }

        public List<Review> GetComment()
        {
            return _context.Reviews.ToList();
        }

        public Review GetById(int id)
        {
            var review = _context.Reviews.FirstOrDefault(x => x.Id == id);

            return review;
        }

        public bool UpdateComment(Review comment)
        {
            _context.Reviews.Update(comment);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Review> GetCommentsOfMovie(int movieId)
        {
            return _context.Reviews.Where(r => r.Movie.Id == movieId).ToList(); 
        }

        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(p => p.Id == id);
        }
    }
}
