using BrewAPI.Models;
using BrewAPI.Services.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// Service responsible for generating JWT tokens
// Encapsulates claims and token creation logic

namespace BrewAPI.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Generates a JWT token for the given user
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            // Send claims in token
            var claims = new ClaimsIdentity(new[]
            {
                // Define claims to include in the token
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.ToString()), // convert enum to string
                new Claim(ClaimTypes.Email, user.Email),
            });

            // Token descriptor create a new instense that defines subject, expiry, issuer, audience, and signing credentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1), // token valid for 1 hour
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                // Sign token with secret key for creating a secure signature
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature) // HMAC SHA256 signature
            };

            var token = tokenHandler.CreateToken(tokenDescriptor); // create security token
            return tokenHandler.WriteToken(token); // WriteToken convert token to string for client usage
        }
    }
}