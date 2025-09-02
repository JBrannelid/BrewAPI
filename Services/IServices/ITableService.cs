using BrewAPI.DTOs.Tables;

namespace BrewAPI.Services.IServices
{
    public interface ITableService
    {
        Task<List<TableDTO>> GetAllTablesAsync();

        Task<TableDTO?> GetTableByIdAsync(int tableId);

        Task<TableDTO?> GetTableByTableNumberAsync(int tableNumber);

        Task<int> CreateTableAsync(TableDTO tableDTO);

        Task<bool> UpdateTableAsync(int tableId, TableDTO tableDTO);

        Task<bool> DeleteTableAsync(int tableId);
    }
}