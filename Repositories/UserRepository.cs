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

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}