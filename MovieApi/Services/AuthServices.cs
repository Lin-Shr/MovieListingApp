using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MovieApi.DTO;
using MovieApp.Areas.Identity.Data;
using MovieApp.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApi.Services
{
    public class AuthServices : IAuth
    {
        private readonly UserManager<MovieAppUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthServices(UserManager<MovieAppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public string GenreateJWT(LoginDTO user)
        {
            var userResult = _userManager.FindByEmailAsync(user.Email).Result;
            var rolename = (_userManager.GetRolesAsync(userResult)).Result.FirstOrDefault();

            IEnumerable<System.Security.Claims.Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role, rolename)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Appsettings:Token").Value));
            SigningCredentials SigningCred = new SigningCredentials
                (securityKey, SecurityAlgorithms.HmacSha256Signature);
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                issuer: _configuration.GetSection("Appsettings:Issuer").Value,
                audience: _configuration.GetSection("Appsettings:Audience").Value,
                signingCredentials: SigningCred
                );
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public async Task<bool> Login(MovieApp.Interfaces.LoginDTO user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            if (identityUser == null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        public async Task<bool> Login(MovieAppUser user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.Email);
            if (identityUser is null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(identityUser, user.PasswordHash);
        }

        public async Task<bool> Register(MovieAppUser user)
        {
            var identityUser = new MovieAppUser
                 {
                    UserName = user.Email,
                    Email = user.Email,
                 };
            var result = await _userManager.CreateAsync(identityUser, user.PasswordHash);
            var role = await _userManager.AddToRoleAsync(identityUser, "User");
            return result.Succeeded && role.Succeeded;
        }
    }
}


