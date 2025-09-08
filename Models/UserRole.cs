namespace BrewAPI.Models
{
    public enum UserRole
    {
        User = 0,
        Admin = 1,
        Manager = 2
    }
}

// Determines access level and permissions throughout the application
// Easy to scale up with different role 