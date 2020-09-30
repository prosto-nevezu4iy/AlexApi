using AlexApi.AppServices.Abstract;
using AlexApi.AppServices.Services;
using AlexApi.Infrasctucture.Extensions;
using AlexApi.Repositories.Repositories;
using AlexAPI.WebServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AlexApi.WebServices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IJwtTokenService _service;

        public LoginController(IUserRepository repository, IJwtTokenService service)
        {
            _repository = repository;
            _service = service;
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Login/Authenticate
        ///     {
        ///        "UserName": "Test",
        ///        "Password": "121212"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the token</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Unauthorized</response> 
        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = _repository.GetAsync(model.UserName, model.Password.CreateMD5());

            if (user != null)
            {
                var tokenString = _service.GenerateJWTToken(model.UserName);
                return Ok(new
                {
                    token = tokenString
                });
            }

            return Unauthorized();
        }

        /// <summary>
        /// Authorize user.
        /// </summary>
        /// <response code="200">Returns the user</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet("Authorize")]
        [Authorize]
        public IActionResult Authorize()
        {
            var user = User.Identity.Name;

            return Ok(new { user = user });
        }
    }
}
