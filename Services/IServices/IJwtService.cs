using BrewAPI.Models;

namespace BrewAPI.Services.IServices
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}