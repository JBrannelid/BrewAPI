using BrewAPI.Models;

namespace BrewAPI.Repositories.IRepositories
{
    public interface ITableRepository
    {
        Task<List<Table>> GetAllTableAsync();

        Task<Table?> GetTableByIdAsync(int tableId);

        Task<Table?> GetTableByTableNumberAsync(int tableNumber);

        Task<int> CreateTableAsync(Table table);

        Task<bool> UpdateTableAsync(Table table);

        Task<bool> DeleteTableAsync(int tableId);
    }
}