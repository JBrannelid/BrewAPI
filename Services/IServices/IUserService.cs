using BrewAPI.DTOs.User;
using BrewAPI.Models;

namespace BrewAPI.Services.IServices
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();

        Task<UserDTO?> GetUserByIdAsync(int userId);

        Task<User?> GetUserByEmailAsync(string email);

        Task<int> CreateUserAsync(UserRegisterDTO userRegisterDTO);

        Task<bool> UpdateUserAsync(int userId, UserDTO userDTO);

        Task<bool> DeleteUserAsync(int userId);

    }
}