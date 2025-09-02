using BrewAPI.DTOs.Bookings;
using BrewAPI.DTOs.Tables;

namespace BrewAPI.Services.IServices
{
    public interface IBookingService
    {
        Task<List<BookingDTO>> GetAllBookingsAsync();

        Task<BookingDTO?> GetBookingByIdAsync(int bookingId);

        Task<List<BookingDTO>> GetBookingsByCustomerIdAsync(int customerId);

        Task<List<BookingDTO>> GetBookingsByTableIdAndDateAsync(int tableId, DateOnly date);

        Task<int?> CreateBookingAsync(CreateBookingDTO createBookingDTO);

        Task<bool> UpdateBookingAsync(int bookingId, UpdateBookingDTO updateBookingDTO);

        Task<bool> DeleteBookingAsync(int bookingId);

        Task<List<TableDTO>> GetAvailableTablesAsync(AvailableTablesRequestDTO request);
    }
}