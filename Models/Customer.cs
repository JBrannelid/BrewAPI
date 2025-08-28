namespace BrewAPI.Models
{
    public class Customer
    {
        public int PK_CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }  
        public virtual List<Booking> Bookings { get; set; } 

    }
}

// Customer entity: stores personal info and links to their bookings.
// Data validation and constraints are handled via Fluent API in DbContext
