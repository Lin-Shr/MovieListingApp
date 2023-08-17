using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using System.IO;
using MovieApp.Interfaces;
using MovieApp.Repository;
using PagedList;
using System.Data;
using System.Xml.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly MovieAppContext _context;
        private readonly IMovies _movieRepository;
        private readonly IGenre _genreRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IGenreMovie _genreMovieRepository;
        public MovieController(IMovies movieRepository, MovieAppContext context, IGenreMovie genreMovieRepository, IWebHostEnvironment webHostEnvironment, IGenre genreRepository)
        {
            _context = context;
            _movieRepository = movieRepository;
            _webHostEnvironment = webHostEnvironment;
            _genreRepository = genreRepository;
            _genreMovieRepository = genreMovieRepository;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var movie = new Movie();
            var genre = _genreRepository.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            MovieGenreVM model = new MovieGenreVM()
            {
                Movie = movie,
                Genres = genre
            };
            var GenreList = new SelectList(_genreRepository.GetAll(), "Id", "Name");
            ViewData["GenreId"] = GenreList;
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(MovieGenreVM movieVM)
        {
            //var genreidinfo = movieVM.Genre.ToList();
            var image = Request.Form.Files.FirstOrDefault();

            var fileName = Guid.NewGuid().ToString();
            var path = $@"images\";

            var wwwRootPath = _webHostEnvironment.WebRootPath;

            var uploads = Path.Combine(wwwRootPath, path);

            var extension = Path.GetExtension(image.FileName);
            var x = Path.Combine(uploads, fileName + extension);
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                image.CopyTo(fileStreams);
            }

            movieVM.Movie.Image = $"\\images\\{fileName}" + extension;

            var isSuccess = _movieRepository.AddMovies(movieVM.Movie);

            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Movie created successfully";
                int movieID = movieVM.Movie.Id;
                foreach (var item in movieVM.Genre)
                {
                    GenreMovie genreMovie = new GenreMovie
                    {
                        MovieId = movieID,
                        GenreId = item
                    };
                    _genreMovieRepository.Add(genreMovie);
                }

                RedirectToAction("GetMovies");
            }



            return RedirectToAction("GetMovies");
        }

        public IActionResult Edit(int id)
        {

            var movie = _movieRepository.GetById(id);
             var genre = _genreRepository.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            MovieGenreVM model = new MovieGenreVM()
            {
                Movie = movie,
                Genres = genre
            };
            var GenreList = new SelectList(_genreRepository.GetAll(), "Id", "Name");
            ViewData["GenreId"] = GenreList;
            return View(model);

           
        }

        [HttpPost]
        public IActionResult Edit(MovieGenreVM movie)
        {
            var image = Request.Form.Files.FirstOrDefault();

            var fileName = Guid.NewGuid().ToString();
            var path = $@"images\";

            var wwwRootPath = _webHostEnvironment.WebRootPath;

            var uploads = Path.Combine(wwwRootPath, path);

            var extension = Path.GetExtension(image.FileName);
            var x = Path.Combine(uploads, fileName + extension);
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                image.CopyTo(fileStreams);
            }

            movie.Movie.Image = $"\\images\\{fileName}" + extension;
            var isSuccess = _movieRepository.UpdateMovies(movie.Movie);
            if (isSuccess)
            {
                RedirectToAction("GetMovies");
            }
            return RedirectToAction("GetMovies");
        }


        public IActionResult Delete(int id)
        {
            var isSuccess = _movieRepository.DeleteMovies(id);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Movie deleted successfully";
                RedirectToAction("GetMovies");

                RedirectToAction("GetMovies");
            }
            return RedirectToAction("GetMovies");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Movie movie = new Movie();
            var serializedModel = TempData["MovieReview"] as string;
            if (string.IsNullOrEmpty(serializedModel))
            {
                movie = _movieRepository.GetById(id);

            }

            else
            {
                TempData["SuccessMessage"] = "Comment Added";

                var moviedeSerialized = JsonConvert.DeserializeObject<Movie>(serializedModel);
                movie = moviedeSerialized;

            }
            //var Genreitems = new SelectList(_genreRepository.GetAll(), "Id", "Name");
            //ViewData["GenreId"] = Genreitems;
            var genre = _movieRepository.GetGenres(id);
            movie.Genres = genre;
            /*ViewBag.Genre = _genreRepository.GetAll();*/
            return View(movie);

        }

        [HttpPost]
        public IActionResult Details(Movie movie)
        {
            return View(movie);
        }
        public ActionResult GetMovies(int? page)
        {
            var movies = _movieRepository.GetMovies().Where(x => !x.IsDeleted);
            return View(movies);

        }
    }
}

