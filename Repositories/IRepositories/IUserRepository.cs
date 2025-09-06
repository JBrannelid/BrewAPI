using BrewAPI.Models;

namespace BrewAPI.Repositories.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}