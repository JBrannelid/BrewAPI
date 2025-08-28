﻿using BrewAPI.Models;

namespace BrewAPI.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();

        Task<User?> GetUserByIdAsync(int userId);

        Task<User?> GetUserByEmailAsync(string email);

        Task<int> CreateUserAsync(User user);

        Task<bool> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int userId);
    }
}