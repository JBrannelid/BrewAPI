using BrewAPI.Data;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

// This repository extends the GenericRepository class 
// CRUD operations are inherited from GenericRepository
// Override specific methods and add additional methods to handle specific business logic

namespace BrewAPI.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BrewAPIDbContext context) : base(context)
        {
        }

        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var rowsAffected = await _dbSet.Where(u => u.UserId == id).ExecuteDeleteAsync();
            return rowsAffected > 0;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}