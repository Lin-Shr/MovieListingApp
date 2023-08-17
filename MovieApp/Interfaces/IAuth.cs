using Microsoft.AspNetCore.Identity;
using MovieApp.Areas.Identity.Data;

namespace MovieApp.Interfaces
{
    public interface IAuth
    {
        string GenreateJWT(LoginDTO user);
        Task<bool> Login(LoginDTO user);

        Task<bool> Register(MovieAppUser user);
    }

    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

