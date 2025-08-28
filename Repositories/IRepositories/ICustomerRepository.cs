using BrewAPI.Models;

namespace BrewAPI.Repositories.IRepositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();

        Task<Customer?> GetCustomerByIdAsync(int id);

        Task<int> AddCustomerAsync(Customer customer);

        Task<bool> UpdateCustomerAsync(Customer customer);

        Task<bool> DeleteCustomerAsync(int customerId);
    }
}