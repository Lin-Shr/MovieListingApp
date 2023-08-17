using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MovieApp.Data;
using MovieApp.Interfaces;
using MovieApp.Repository;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class EmailController : ControllerBase

        {
            private readonly MovieAppContext _context;
            private readonly IGenre _genreRepository;
            private readonly IWebHostEnvironment _webHostEnvironment;
            private readonly IGenreMovie _genreMovieRepository;
            private readonly IEmail _emailRepository;



            public EmailController(MovieAppContext context, 
                IGenreMovie genreMovieRepository, 
                IWebHostEnvironment webHostEnvironment, 
                IGenre genreRepository, 
                IEmail emailRepository)
            {
                _context = context;
                _webHostEnvironment = webHostEnvironment;
                _genreRepository = genreRepository;
                _genreMovieRepository = genreMovieRepository;
                _emailRepository = emailRepository;
            }
            [HttpPost("Email")]
            public async Task<IActionResult> SendEmailAsync(string body)
            {
                await _emailRepository.SendEmailAsync(body);
                return Ok();
            }
        }
        
    
}
