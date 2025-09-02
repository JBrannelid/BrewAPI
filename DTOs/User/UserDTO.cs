using BrewAPI.Models;

namespace BrewAPI.DTOs.User
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public UserRole Role { get; set; }
    }
}