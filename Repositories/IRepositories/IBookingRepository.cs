using BrewAPI.Models;

namespace BrewAPI.Repositories.IRepositories
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<List<Booking>> GetBookingsByCustomerIdAsync(int customerId);

        Task<List<Booking>> GetBookingsByTableIdAndDateAsync(int tableId, DateOnly date); 
    }
}