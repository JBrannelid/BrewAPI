namespace BrewAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public string PasswordHash { get; set; }
    }
}

// User entity: stores information about a user
// Role determines access level store in a separate table UserRole.cs with columns (Admin, Manager, Customer) 
// Data validation and constraints are handled via Fluent API in DbContext