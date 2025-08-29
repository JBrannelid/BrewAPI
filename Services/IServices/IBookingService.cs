using BrewAPI.DTOs;

namespace BrewAPI.Services.IServices
{
    public interface IBookingService
    {
        Task<List<BookingDTO>> GetAllBookingsAsync();

        Task<BookingDTO?> GetBookingByIdAsync(int bookingId);

        Task<List<BookingDTO>> GetBookingsByCustomerIdAsync(int customerId);

        Task<List<BookingDTO>> GetBookingsByTableIdAndDateAsync(int tableId, DateOnly date);

        Task<int?> CreateBookingAsync(CreateBookingDto createBookingDto);

        Task<bool> UpdateBookingAsync(int bookingId, UpdateBookingDto updateBookingDto);

        Task<bool> DeleteBookingAsync(int bookingId);

        Task<List<TableDTO>> GetAvailableTablesAsync(AvailableTablesRequestDto request);
    }
}