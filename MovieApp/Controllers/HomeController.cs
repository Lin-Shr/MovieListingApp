using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Areas.Identity.Data;
using MovieApp.Models;
using System;
using System.Diagnostics;

namespace MovieApp.Controllers
{
 /*   private readonly UserManager<MovieAppUser> _userManager;
    
    public HomeController(SignInManager<MovieAppUser> signInManager)
    {
        _userManager = userManager;
    }*/
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
/*        public IActionResult AddMovies()
        {
            return View();
        }*/
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}