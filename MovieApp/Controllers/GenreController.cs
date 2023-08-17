using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Areas.Identity.Data;
using MovieApp.Interfaces;
using MovieApp.Repository;

namespace MovieApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenreController : Controller
    {
        private readonly IGenre _genreRepository;
        public GenreController(IGenre genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var genre = new Genre();
            return View(genre);
        }
        [HttpGet]
            public IActionResult GetAll(int? page) 
        {
            var genre = _genreRepository.GetAll().Where(x => !x.IsDeleted);
            return View(genre);
        }
        [HttpGet]
        public IActionResult Edit() 
        {
        return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            var isSuccess = _genreRepository.AddGenre(genre);
            return RedirectToAction("GetAll");


        }
        public IActionResult Edit(Genre genre)
        {
            var isSuccess = _genreRepository.UpdateGenre(genre);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Genre Edited";
                RedirectToAction("GetMovies");
            }
            return RedirectToAction("GetAll");

        }
        public IActionResult Delete(int id)
        {
            var isSuccess = _genreRepository.DeleteGenre(id);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Genre deleted successfully";
                RedirectToAction("GetAll");

                RedirectToAction("GetAll");
            }
            return RedirectToAction("GetAll");
        }
        public ActionResult GetGenre(int? page)
        {
            var movies = _genreRepository.GetAll().Where(x => !x.IsDeleted);
            return View(movies);

        }
    }
}
