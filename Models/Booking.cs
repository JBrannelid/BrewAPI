namespace BrewAPI.Models
{
    public class Booking : IEntity
    {
        public int Id { get; set; }
        public int FK_CustomerId { get; set; }
        public int FK_TableId { get; set; }
        public Customer? Customer { get; set; }
        public Table? Table { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
        public int NumberGuests { get; set; }
        public string? Status { get; set; }
        public TimeSpan DurationTime { get; set; }
    }
}

// Booking entity: stores reservation details including reference to customer and table
// Data validation and constraints are handled via Fluent API in DbContext