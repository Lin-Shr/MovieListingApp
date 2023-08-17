using AutoMapper;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using MovieApi.DTO;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Interfaces;
using MovieApp.Migrations;
using MovieApp.Models;
using System.Data;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly MovieAppContext _context;
        private readonly IMovies _movieRepository;
        private readonly IGenre _genreRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IGenreMovie _genreMovieRepository;
        private readonly IMapper _mapper;

        public MovieController(IMovies movieRepository, MovieAppContext context, IGenreMovie genreMovieRepository, IWebHostEnvironment webHostEnvironment, IGenre genreRepository, IMapper mapper)
        {
            _context = context;
            _movieRepository = movieRepository;
            _webHostEnvironment = webHostEnvironment;
            _genreRepository = genreRepository;
            _genreMovieRepository = genreMovieRepository;
            _mapper = mapper;

        }
        [HttpGet("{movieId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieDTO>))]
        public IActionResult GetById(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
            {
                return NotFound("Movie Not Found");
            }
            var movie = _mapper.Map<MovieDTO>(_movieRepository.GetById(movieId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(movie);

        }
        [HttpGet("Name")]
        [ProducesResponseType(200, Type = typeof(MovieDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetByName(string name)
        {
            if (!_movieRepository.MovieExists(name))
            {
                return NotFound("No Movie of that name is found");
            }
            var pokemon = _mapper.Map<MovieDTO>(_movieRepository.GetByName(name));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemon);
        }

        //Sarthak
        [HttpGet("All Movies")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieDTO>))]
        public ActionResult<List<MovieDTO>> GetMoviesList()
        {
            var getmovieslist = _movieRepository.GetMovies().Where(x => !x.IsDeleted);
            var result = getmovieslist.Select(x => _mapper.Map<MovieDTO>(x)).ToList();
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddMovie([FromBody] MovieDTO addMovie)
        {
            if (addMovie == null)
            {
                return BadRequest(ModelState);
            }
            var movie = _movieRepository.GetMovies()
                .Where(c => c.Title.Trim().ToUpper() == addMovie.Title.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (movie != null)
            {
                ModelState.AddModelError("", "Movie already exists!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movieMap = _mapper.Map<Movie>(addMovie);
            if (!_movieRepository.AddMovies(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong! Please try again");
                return StatusCode(500, ModelState);
            }
            return Ok("Movie Successfully Added");
        }
        [HttpDelete("MovieID")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public IActionResult DeleteMovie(int movieId)
        {
            if (!_movieRepository.MovieExists(movieId))
            {
                return NotFound("Genre not found");
            }
            var deleteGenre = _genreRepository.GetById(movieId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_movieRepository.DeleteMovies(movieId))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Movie Deleted Successfull");
        }
        [HttpPut("Update-Movie")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGenre(int movieId, [FromBody] MovieDTO updateMovie)
        {
            if (updateMovie == null)
            {
                return BadRequest(ModelState);
            }
            if (movieId != updateMovie.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_genreRepository.GenreExists(movieId))
            {
                return NotFound();
            }
            var movieMap = _mapper.Map<Movie>(updateMovie);

            if (!_movieRepository.UpdateMovies(movieMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Movie Edited Successfully");
        }
    }
}