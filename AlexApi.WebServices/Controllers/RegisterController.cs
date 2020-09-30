using AlexApi.Domain.Models;
using AlexApi.Repositories.Repositories;
using AlexAPI.WebServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AlexApi.Infrasctucture.Extensions;

namespace AlexAPI.WebServices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public RegisterController(IUserRepository repository)
        {
            _repository = repository;
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
        ///        "Password": "121212"
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

            _repository.AddAsync(user);

            return Ok();
        }
    }
}
