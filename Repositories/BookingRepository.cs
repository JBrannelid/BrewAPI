using BrewAPI.Data;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

// This repository extends the GenericRepository class 
// CRUD operations are inherited from GenericRepository
// Override specific methods and add additional methods to handle specific business logic

namespace BrewAPI.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BrewAPIDbContext context) : base(context)
        {
        }

        public override async Task<Booking?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .FirstOrDefaultAsync(b => b.PK_BookingId == id);
        }

        public override async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _dbSet
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .ToListAsync();
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var rowsAffected = await _dbSet
                .Where(b => b.PK_BookingId == id).ExecuteDeleteAsync();
            return rowsAffected > 0;
        }

        // Navigation methods
        public async Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            return await _dbSet
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .Where(b => b.FK_CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByTableIdAndDateAsync(int tableId, DateOnly date)
        {
            return await _dbSet
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .Where(b => b.FK_TableId == tableId && b.BookingDate == date)
                .ToListAsync();
        }
    }
}