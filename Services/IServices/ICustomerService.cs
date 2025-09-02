using BrewAPI.DTOs.Customers;

namespace BrewAPI.Services.IServices
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllCustomersAsync();

        Task<CustomerDTO?> GetCustomerByIdAsync(int customerId);

        Task<int> CreateCustomerAsync(CreateCustomerDTO createCustomerDTO);

        Task<bool> UpdateCustomerAsync(int customerId, UpdateCustomerDTO updateCustomerDTO);

        Task<bool> DeleteCustomerAsync(int customerId);
    }
}