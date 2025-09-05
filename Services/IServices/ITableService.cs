using BrewAPI.DTOs.Tables;

namespace BrewAPI.Services.IServices
{
    public interface ITableService
    {
        Task<List<GetTableDTO>> GetAllTablesAsync();

        Task<TableDTO?> GetTableByIdAsync(int tableId);

        Task<int> CreateTableAsync(CreateTableDTO createTableDTO);

        Task<bool> UpdateTableAsync(int tableId, UpdateTableDTO updateTableDTO);

        Task<bool> DeleteTableAsync(int tableId);
    }
}