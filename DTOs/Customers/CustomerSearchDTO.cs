namespace BrewAPI.DTOs.Customers
{
    public class CustomerSearchDTO
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DisplayText => $"{Name} - {PhoneNumber} - {Email}";
    }
}