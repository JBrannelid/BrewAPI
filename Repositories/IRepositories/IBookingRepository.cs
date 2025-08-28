using BrewAPI.Models;

namespace BrewAPI.Repositories.IRepositories
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllBookingsAsync();

        Task<Booking?> GetBookingByIdAsync(int bookingId);

        Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId);

        Task<List<Booking>> GetBookingsByTableIdAndDateAsync(int tableId, DateOnly date);

        Task<int> CreateBookingAsync(Booking booking);

        Task<bool> UpdateBookingAsync(Booking booking);

        Task<bool> DeleteBookingAsync(int bookingId);
    }
}