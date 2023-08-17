using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieApi.DTO;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Interfaces;

namespace MovieApi.Controllers
{
    public class ReviewController : Controller
    {
        private readonly MovieAppContext _context;
        private readonly IGenre _genreRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IReviews _reviewRepository;
        private readonly IMovies _movieRepository;
        private readonly IGenreMovie _genreMovieRepository;
        private readonly IMapper _mapper;

        public ReviewController(MovieAppContext context,
            IGenreMovie genreMovieRepository,
            IMovies movieRepository,
            IWebHostEnvironment webHostEnvironment,
            IGenre genreRepository,
            IMapper mapper,
            IReviews reviewRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _genreRepository = genreRepository;
            _movieRepository = movieRepository;
            _genreMovieRepository = genreMovieRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;

        }
        [HttpPost("Review/MovieId")]
        [ProducesResponseType(204),]
        [ProducesResponseType(400)]
        public IActionResult AddComment([FromQuery] int movieId, [FromBody] ReviewDTO addReview)
        {
            if (addReview == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(addReview);
            reviewMap.MovieId = movieId;
            var isAdded = _reviewRepository.AddComment(reviewMap);
            return Ok("Review Successfully Added");
        }
        [HttpPut("Update/Review/MovieId")]
        [ProducesResponseType(204),]
        [ProducesResponseType(400)]
        public IActionResult UpdateComment(int reviewId, [FromQuery] int movieId, [FromBody] ReviewDTO updateReview)
        {
            if (updateReview == null)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            if (reviewId != updateReview.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(updateReview);
            reviewMap.MovieId = movieId;
            if (!_reviewRepository.UpdateComment(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Review Edited Successfully");

        }
        [HttpGet("Rating/Movie")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]

        public IActionResult GetMovieRating(string name)
        {

            if (!_movieRepository.MovieExists(name))
            {
                return NotFound("Movie Not Found");
            }
            var rating = _movieRepository.GetMovieRating(name);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rating);
        }
        [HttpDelete("ReviewID")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public IActionResult UpdateGenre(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound("Review not found");
            }
            var deleteGenre = _reviewRepository.GetById(reviewId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.DeleteComment(reviewId))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Review Deleted Successfull");
        }
    }
}
