using Cards.DTOs;
using Cards.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _repository;
        public AuthenticationController(IAuthenticationService repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {
            var user = await _repository.GetUserAsync(request.Email);

            if (user != null)
            {
                return BadRequest("user with provided email exists");
            }

            await _repository.CreateUserAsync(request);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequest request)
        {
            var user = await _repository.GetUserAsync(request.Email);

            if (user == null)
            {
                return NotFound("user not found");
            }

            var isPasswordOk = _repository.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

            if(isPasswordOk == false)
            {
                return BadRequest("invalid username or password");
            }

            var jwtToken = await _repository.LoginAsync(request);

            return Ok(jwtToken);
        }
    }
}
