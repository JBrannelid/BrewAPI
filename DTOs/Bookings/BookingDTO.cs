using BrewAPI.DTOs.Customers;
using BrewAPI.DTOs.Tables;

namespace BrewAPI.DTOs.Bookings
{
    public class BookingDTO
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int NumberGuests { get; set; }
        public string Status { get; set; }
        public int TableId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
        public TimeSpan DurationTime { get; set; }
        public CustomerDTO? Customer { get; set; }
        public TableDTO? Table { get; set; }
    }
}