using BrewAPI.DTOs;

namespace BrewAPI.Services.IServices
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(UserRegisterDTO userRegisterDTO);
        Task<string?> LoginUserAsync(LoginUserDTO loginUserDTO);
    }
}
