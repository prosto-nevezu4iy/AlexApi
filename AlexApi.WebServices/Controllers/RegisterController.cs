using AlexApi.Domain.Models;
using AlexApi.Repositories.Repositories;
using AlexAPI.WebServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AlexApi.Infrasctucture.Extensions;
using AlexApi.Repositories.Interfaces;
using System.Diagnostics;
using Serilog;

namespace AlexAPI.WebServices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly Stopwatch _processTimer;

        public RegisterController(IUserRepository repository)
        {
            _repository = repository;
            _processTimer = new Stopwatch();
        }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Register
        ///     {
        ///        "UserName": "Test",
        ///        "Password": "121212",
        ///        "ClientId": "DefaultConnection"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the success message</response>
        /// <response code="400">Bad Request</response> 
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new User()
            {
                UserName = model.UserName,
                Password = model.Password.CreateMD5()
            };

            _processTimer.Start();
            _repository.AddAsync(user, model.ClientId);
            _processTimer.Stop();
            
            Log.Information($"Duration of query was {_processTimer.ElapsedMilliseconds}ms and query name was AddAsync");

            return Ok();
        }
    }
}
