using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApi.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MovieApp.Interfaces;
using AutoMapper;
using MovieApp.Repository;
using MovieApp.Areas.Identity.Data;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<MovieAppUser> _userManager;
        private readonly IAuth _auth;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(IAuth auth, IMapper mapper, IConfiguration configuration, UserManager<MovieAppUser> userManager)
        {
            _auth = auth;
            _mapper = mapper;
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterDTO>> Register(RegisterDTO user)
        {
            var Idenuser = _mapper.Map<MovieAppUser>(user);
            if (await _auth.Register(Idenuser))
            {
                return Ok("Successfully Done");
            }
            return BadRequest("Something Went Wrong");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginDTO>> Login(LoginDTO user)
        {
            var result = await _auth.Login(user);
            if (result)
            {
                var token = _auth.GenreateJWT(user);
                return Ok(token);
            }
            return BadRequest("User Not Found");
        }

       /* private async Task<String> CreateJwtToken(LoginDTO user)
        {
            var Idenuser = await _userManager.FindByEmailAsync(user.Email);
            var token = _configuration["AppSettings:Token"];
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token));
            var signInCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
          

            if (Idenuser != null && await _userManager.CheckPasswordAsync(Idenuser, user.Password))
            {
                var claims = new List<Claim>
                     {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name,  Idenuser.UserName)
                         };
                var jwt = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: signInCredentials);
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                return encodedJwt;
            }
            return "";

        }*/

    }

}
