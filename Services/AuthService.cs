using BrewAPI.DTOs.Auth;
using BrewAPI.DTOs.User;
using BrewAPI.Services.IServices;

// Service responsible for authentication operations (registration/login)
// Handles logic for users and JWT token generation

namespace BrewAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        // DI of UserService and JwtService for user management and token generation
        public AuthService(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        // Registers a new user
        public async Task<bool> RegisterUserAsync(UserRegisterDTO userRegisterDTO)
        {
            // Check if email already exists. Return false if it does
            var existingUser = await _userService.GetUserByEmailAsync(userRegisterDTO.Email);
            if (existingUser != null)
            {
                return false; // Email is already in use
            }

            // Create new user via UserService
            await _userService.CreateUserAsync(userRegisterDTO);
            return true;
        }

        // Logs in a user. Returns JWT token string if login succeeds, null otherwise
        public async Task<string?> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            // Verify email address
            var user = await _userService.GetUserByEmailAsync(loginUserDTO.Email);
            if (user == null)
            {
                return null; // Invalid email
            }

            // Verify password
            bool passwordMatch = await ValidatePasswordAsync(loginUserDTO.Email, loginUserDTO.Password);
            if (!passwordMatch)
            {
                return null; // Invalid password
            }

            // Generate JWT token using JwtService for authenticated user
            var token = _jwtService.GenerateJwtToken(user);
            return token;
        }

        // Validates password for given email
        // Private because this is used within AuthService class only
        private async Task<bool> ValidatePasswordAsync(string email, string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            // Uses BCrypt to securely compare hashed password
            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}