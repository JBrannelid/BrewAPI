using BrewAPI.Data;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BrewAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BrewAPIDbContext _context;

        public BookingRepository(BrewAPIDbContext context)
        {
            _context = context;
        }

        // Adds a new booking to the Db and return Id of the new booking
        public async Task<int> CreateBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking.PK_BookingId;
        }

        // Deletes a booking by Id
        // Returns true if a row was affected, false otherwise
        public async Task<bool> DeleteBookingAsync(int bookingId)
        {
            var rowsAffected = await _context.Bookings
                .Where(b => b.PK_BookingId == bookingId).ExecuteDeleteAsync();
            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        // Retrieves a list of all bookings related to a Customer and Table data
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .ToListAsync();
            return bookings;
        }

        // Retrieves a single booking by its Id
        // Returns null (FirstOrDefault) if the booking does not exist
        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .FirstOrDefaultAsync(b => b.PK_BookingId == bookingId);
            return booking;
        }

        // Retrieves all bookings for a specific customer
        public async Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .Where(b => b.FK_CustomerId == customerId)
                .ToListAsync();
            return bookings;
        }

        // Retrieves all bookings for a specific table on a specific date
        public async Task<List<Booking>> GetBookingsByTableIdAndDateAsync(int tableId, DateOnly date)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table)
                .Where(b => b.FK_TableId == tableId && b.BookingDate == date)
                .ToListAsync();
            return bookings;
        }

        // Updates an existing booking in the Db
        // Returns true if any row was affected, false if no changes were mad
        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            var result = await _context.SaveChangesAsync();
            if (result != 0)
            {
                return true;
            }
            return false;
        }
    }
}