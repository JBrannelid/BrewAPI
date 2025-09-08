using BrewAPI.DTOs.Customers;
using BrewAPI.Extensions.Mapping;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using BrewAPI.Services.IServices;

namespace BrewAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => c.MapToCustomerDto()).ToList();
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            return customer?.MapToCustomerDto();
        }

        public async Task<int> CreateCustomerAsync(CreateCustomerDTO createCustomerDTO)
        {
            var customer = createCustomerDTO.MapToCustomer();
            var createdCustomer = await _customerRepository.CreateAsync(customer);
            return createdCustomer.Id;
        }

        public async Task<bool> UpdateCustomerAsync(int customerId, UpdateCustomerDTO updateCustomerDTO)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(customerId);
            if (existingCustomer == null)
            {
                return false;
            }

            updateCustomerDTO.MapToCustomer(existingCustomer);
            await _customerRepository.UpdateAsync(existingCustomer);
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            return await _customerRepository.DeleteAsync(customerId);
        }

        public async Task<List<CustomerSearchDTO>> SearchCustomersAsync(string searchTerm)
        {
            return await _customerRepository.SearchAsync(searchTerm);
        }
    }
}