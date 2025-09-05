using BrewAPI.Data;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BrewAPI.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly BrewAPIDbContext _context;

        public TableRepository(BrewAPIDbContext context)
        {
            _context = context;
        }

        // Adds a new table to the Db and returns the primary key (Id) of the created table
        public async Task<int> CreateTableAsync(Table table)
        {
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return table.PK_TableId;
        }

        // Deletes a table by Id. Returns true if a row was affected, false if no row was affected
        public async Task<bool> DeleteTableAsync(int tableId)
        {
            var rowsAffected = await _context.Tables.Where(t => t.PK_TableId == tableId).ExecuteDeleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        // Retrieves a list of all tables from the Db
        public async Task<List<Table>> GetAllTableAsync()
        {
            var tables = await _context.Tables.ToListAsync();
            return tables;
        }

        // Retrieves a single table by Id. Returns null (FirstOrDefault) if the table does not exist
        public async Task<Table?> GetTableByIdAsync(int tableId)
        {
            var table = await _context.Tables
                .FirstOrDefaultAsync(t => t.PK_TableId == tableId);
            return table;
        }

        // Updates an existing table in the Db. Returns true if any row was affected, false if no changes were made
        public async Task<bool> UpdateTableAsync(Table table)
        {
            _context.Tables.Update(table);
            var result = await _context.SaveChangesAsync();
            if (result != 0)
            {
                return true;
            }
            return false;
        }
    }
}