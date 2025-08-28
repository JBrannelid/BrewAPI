using BrewAPI.DTOs;

namespace BrewAPI.Services.IServices
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllCustomersAsync();

        Task<CustomerDTO?> GetCustomerByIdAsync(int customerId);

        Task<int> CreateCustomerAsync(CustomerDTO customerDTO);

        Task<bool> UpdateCustomerAsync(int customerId, CustomerDTO customerDTO);

        Task<bool> DeleteCustomerAsync(int customerId);
    }
}