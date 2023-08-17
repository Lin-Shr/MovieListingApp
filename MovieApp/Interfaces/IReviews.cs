using MovieApp.Areas.Identity.Data;

namespace MovieApp.Interfaces
{
    public interface IReviews
    {
       // bool AddRating(Review rating);
        bool AddComment(Review comment);
        /*bool UpdateRating(Review rating);*/
        bool UpdateComment(Review comment);
        bool DeleteComment(int id);
        Review GetById(int id);
        List<Review> GetComment();
        ICollection<Review> GetCommentsOfMovie(int movieId);
        bool ReviewExists(int id);
    }
}
