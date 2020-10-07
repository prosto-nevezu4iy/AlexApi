using AlexApi.AppServices;
using AlexApi.Domain.Models;
using AlexApi.Infrasctucture.Extensions;
using AlexApi.Repositories.Interfaces;
using AlexApi.Repositories.Repositories;
using AlexAPI.WebServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Diagnostics;

namespace AlexApi.WebServices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IJwtTokenService _service;
        private readonly Stopwatch _processTimer;

        public LoginController(IUserRepository repository, IJwtTokenService service)
        {
            _repository = repository;
            _service = service;
            _processTimer = new Stopwatch();
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
        ///        "Password": "121212",
        ///        "ClientId": "DefaultConnection"
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

            if(model.UserName == "Alex")
            {
                /*throw new ArgumentException(
                    $"We can't login user with name {model.UserName}");*/
            }

            var userCredentials = new User()
            {
                UserName = model.UserName,
                Password = model.Password.CreateMD5()
            };

            _processTimer.Start();
            var user = _repository.GetAsync(userCredentials, model.ClientId);
            _processTimer.Stop();

            Log.Information($"Duration of query was {_processTimer.ElapsedMilliseconds}ms and query name was GetAsync");

            if (user.Result != null)
            {
                _processTimer.Start();
                var tokenString = _service.GenerateJWTToken(model.UserName);
                _processTimer.Stop();
                Log.Information($"Duration of encryption of JWT token was {_processTimer.ElapsedMilliseconds}ms");

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
