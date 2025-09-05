using BrewAPI.DTOs.Auth;
using BrewAPI.DTOs.User;
using BrewAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

// Controller responsible for authentication endpoints (register & login)
// TODO: A better global error and logging handeling

namespace BrewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        // Using DI to decouple controller from implementation
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/Auth/Register
        // DTO to prevent exposing the internal User entity
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO newUser)
        {
            var success = await _authService.RegisterUserAsync(newUser);
            if (!success)
            {
                // Returning 401 (Unauthorized)
                return BadRequest("Email is already in use");
            }
            //Returning 201 (Created)
            return Created();
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUser)
        {
            var token = await _authService.LoginUserAsync(loginUser);
            if (token == null)
            {
                // Returning 401 (Unauthorized)
                return Unauthorized("Invalid email or password");
            }

            return Ok(new { token });
        }
    }
}