using BrewAPI.DTOs.User;
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
            var users = await _userRepository.GetAllUsersAsync();
            var userDTOs = users.Select(u => new UserDTO
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role
            }).ToList();

            return userDTOs;
        }

        // Retrieves a user by their ID or returns null if not found
        public async Task<UserDTO?> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            var userDTO = new UserDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };

            return userDTO;
        }

        // Retrieves a user by their email (email entity) or returns null if not found
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        // Creates a new user and returns the new user's ID (primary key)
        public async Task<int> CreateUserAsync(UserRegisterDTO userRegisterDTO)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password);

            var user = new User
            {
                FirstName = userRegisterDTO.FirstName,
                LastName = userRegisterDTO.LastName,
                Email = userRegisterDTO.Email,
                Role = UserRole.User,
                PasswordHash = passwordHash
            };

            var newUserId = await _userRepository.CreateUserAsync(user);
            return newUserId;
        }

        // Updates an existing user's details. Returns true if update succeeded, false if user not found
        public async Task<bool> UpdateUserAsync(int userId, UserDTO userDTO)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.FirstName = userDTO.FirstName;
            existingUser.LastName = userDTO.LastName;
            existingUser.Email = userDTO.Email;
            existingUser.Role = userDTO.Role;

            return await _userRepository.UpdateUserAsync(existingUser);
        }

        // Deletes a user by their ID. Returns true if deletion succeeded, false if user not found
        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }
    }
}