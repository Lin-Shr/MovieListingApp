using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Interfaces;
using MovieApp.Repository;
using Newtonsoft.Json;

namespace MovieApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviews _reviewRepository;
        private readonly IMovies _movieRepository;


        public ReviewController(IReviews reviewRepository, MovieAppContext context, IMovies movieRepository)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
        }

        [HttpPost]
        public IActionResult Create(Review review) 
        {
            var isSuccess = _reviewRepository.AddComment(review);
            if (isSuccess)
            {
                int x = review.MovieId;
                var movie = _movieRepository.GetById(x);
                return RedirectToAction("Details", "Movie", new { id = x });
            }
            
                return RedirectToAction("Details", "Movie");


       }
        public IActionResult Edit(Review review)
        {
            var isSuccess = _reviewRepository.UpdateComment(review);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Comment Edited";
                RedirectToAction("Details");
            }
            return RedirectToAction("Details");

        }
      /*  public IActionResult AddRating(Review rating) 
        {
            var isSuccess = _reviewRepository.AddRating(rating);
            if (isSuccess)
            {
                int x = rating.MovieId;
                var movie = _movieRepository.GetById(x);
                return RedirectToAction("Details", "Movie", new { id = x });
            }
            return RedirectToAction("Details", "Movie");
        }*/

    }
}
