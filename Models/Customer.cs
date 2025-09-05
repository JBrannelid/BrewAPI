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

// Customer entity: 
// represents contact information for reservation purposes

// Distinct from User entity - Customer stores booking contact details for walk-in guests
// bookings without requiring system registration

// User entity handles authentication/authorization for staff and registered system users

// Data validation and constraints are handled via Fluent API in DbContext
