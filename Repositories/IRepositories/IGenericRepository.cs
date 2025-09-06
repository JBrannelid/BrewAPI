using BrewAPI.Models;

namespace BrewAPI.Repositories.IRepositories
{
    // Generic repository interface
    // Collect all DB interaction in one place. Keep the code scalable and DRY
    // Implement this interface through DI and call the corresponding function

    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
