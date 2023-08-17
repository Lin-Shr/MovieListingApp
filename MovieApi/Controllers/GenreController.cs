using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApi.DTO;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Interfaces;
using MovieApp.Repository;
using System.Data;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly MovieAppContext _context;
        private readonly IGenre _genreRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IGenreMovie _genreMovieRepository;
        private readonly IMapper _mapper;

        public GenreController(MovieAppContext context, IGenreMovie genreMovieRepository, IWebHostEnvironment webHostEnvironment, IGenre genreRepository, IMapper mapper)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _genreRepository = genreRepository;
            _genreMovieRepository = genreMovieRepository;
            _mapper = mapper;

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{genreId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GenreDTO>))]
        public IActionResult GetById(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
            {
                return NotFound("Genre Not Found");
            }
            var genre = _mapper.Map<GenreDTO>(_genreRepository.GetById(genreId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(genre);

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("AllGenre")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GenreDTO>))]
        public ActionResult<List<GenreDTO>> GetMoviesList()
        {
            var getmovieslist = _genreRepository.GetAll().Where(x => !x.IsDeleted);
            var result = getmovieslist.Select(x => _mapper.Map<GenreDTO>(x)).ToList();
            return Ok(result);
        }

        [HttpGet("Movie/GenreId")]
        [ProducesResponseType(200, Type = typeof(List<Movie>))]
        [ProducesResponseType(400)]
        public IActionResult GetMoviesByGenre(int genreId)
        {
            if(! _genreRepository.GenreExists(genreId))
            {
                return NotFound("Genre not found");
            }
            var movies = _mapper.Map<List<MovieDTO>>(_genreRepository.GetMovies(genreId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(movies);

        }
        [HttpPost("Add-Genre")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult AddGenre([FromBody] GenreDTO addGenre)
        {
            if (addGenre == null)
            {
                return BadRequest(ModelState);
            }
            var genre = _genreRepository.GetAll()
                .Where(c => c.Name.Trim().ToUpper() == addGenre.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if(genre != null)
            {
                ModelState.AddModelError("", "Genre already exists!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(addGenre);
            if (!_genreRepository.AddGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong! Please try again");
                return StatusCode(500, ModelState);
            }
            return Ok("Genre Successfully Added");
        }
        [HttpPut("Update-Genre")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGenre(int genreId, [FromBody ] GenreDTO updateGenre) 
        {
            if (updateGenre == null)
            {
                return BadRequest(ModelState);
            }
            if(genreId != updateGenre.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_genreRepository.GenreExists(genreId))
            {
                return NotFound();
            }
            var genreMap = _mapper.Map<Genre>(updateGenre);

            if (!_genreRepository.UpdateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Genre Edited Successfully"); 
        }
        [HttpDelete("GenreID")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public IActionResult UpdateGenre(int genreId)
        {
            if (!_genreRepository.GenreExists(genreId))
            {
                return NotFound("Genre not found");
            }
            var deleteGenre = _genreRepository.GetById(genreId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_genreRepository.DeleteGenre(genreId))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Genre Deleted Successfull");
        }






    }

}
