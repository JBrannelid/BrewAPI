using BrewAPI.Models;

namespace BrewAPI.Repositories.IRepositories
{
    public interface IMenuItemRepository : IGenericRepository<MenuItem>
    {
        Task<List<MenuItem>> GetMenuItemByCategoryAsync(string category);
    }
}