using BrewAPI.DTOs.User;
using BrewAPI.Extensions.Mapping;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using BrewAPI.Services.IServices;

namespace BrewAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Retrieves all users from the database
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => u.MapToUserDto()).ToList();
        }

        // Retrieves a user by their ID or returns null if not found
        public async Task<UserDTO?> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user?.MapToUserDto();
        }

        // Retrieves a user by their email (email entity) or returns null if not found
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        // Creates a new user and returns the new user's ID (primary key)
        public async Task<int> CreateUserAsync(UserRegisterDTO userRegisterDTO)
        {
            var user = userRegisterDTO.MapToUser();
            var createdUser = await _userRepository.CreateAsync(user);
            return createdUser.UserId;
        }

        // Updates an existing user's details. Returns true if update succeeded, false if user not found
        public async Task<bool> UpdateUserAsync(int userId, UserDTO userDTO)
        {
            var existingUser = await _userRepository.GetByIdAsync(userId);
            if (existingUser == null)
            {
                return false;
            }

            userDTO.MapToUser(existingUser);
            await _userRepository.UpdateAsync(existingUser);
            return true;
        }

        // Deletes a user by their ID. Returns true if deletion succeeded, false if user not found
        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }
    }
}