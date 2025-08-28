using BrewAPI.Data;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BrewAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BrewAPIDbContext _context;

        public UserRepository(BrewAPIDbContext context)
        {
            _context = context;
        }

        // Retrieves a list of all users from the database
        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        // Retrieves a single user by Id. Returns null (FirstOrDefaultAsync) if the user does not exist
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
            return user;
        }

        // Retrieves a single user by email. Returns null if no matching user is found
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        // Adds a new user to the Db and returns the primary key (Id) of the created user
        public async Task<int> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        // Updates an existing user in the Db. Returns true if a row was affected, false if no changes were made
        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            var result = await _context.SaveChangesAsync();
            if (result != 0)
            {
                return true;
            }
            return false;
        }

        // Deletes a user by Id. Returns true if a row was affected, false if no row was affected
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var rowsAffected = await _context.Users.Where(u => u.UserId == userId).ExecuteDeleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}