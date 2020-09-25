using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuthentication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private IEnumerable<User> _users = new List<User>(){
            new User { UserName = "Alex", Password = "121212" },
            new User { UserName = "Ivan", Password = "Mentor" }
        };

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] User login)
        {
            User user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                return Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return Unauthorized();
        }

        [HttpGet("Authorize")]
        [Authorize(Policy = "ValidAccessToken")]
        public IActionResult Authorize()
        {
            var user = User.Identity.Name;

            return Ok(new { user = user });
        }

        private User AuthenticateUser(User loginCredentials)
        {
            return _users.SingleOrDefault(x => 
                x.UserName == loginCredentials.UserName 
                && x.Password == loginCredentials.Password);
        }

        private string GenerateJWTToken(User userInfo)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userInfo.UserName)
                }),
                Expires = DateTime.UtcNow.AddYears(2),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
