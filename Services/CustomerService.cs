using BrewAPI.DTOs.Customers;
using BrewAPI.Models;
using BrewAPI.Repositories.IRepositories;
using BrewAPI.Services.IServices;

namespace BrewAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepository; 

        public CustomerService(IGenericRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            var customerDTOs = customers.Select(c => new CustomerDTO
            {
                CustomerId = c.PK_CustomerId,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email
            }).ToList();

            return customerDTOs;
        }

        public async Task<CustomerDTO?> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                return null;
            }

            var customerDTO = new CustomerDTO
            {
                CustomerId = customer.PK_CustomerId,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email
            };

            return customerDTO;
        }

        public async Task<int> CreateCustomerAsync(CreateCustomerDTO createCustomerDTO)
        {
            var customer = new Customer
            {
                Name = createCustomerDTO.Name,
                PhoneNumber = createCustomerDTO.PhoneNumber,
                Email = createCustomerDTO.Email
            };

            var createdCustomer = await _customerRepository.CreateAsync(customer);
            return createdCustomer.PK_CustomerId;
        }

        public async Task<bool> UpdateCustomerAsync(int customerId, UpdateCustomerDTO updateCustomerDTO)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(customerId);
            if (existingCustomer == null)
            {
                return false;
            }

            existingCustomer.Name = updateCustomerDTO.Name;
            existingCustomer.PhoneNumber = updateCustomerDTO.PhoneNumber;
            existingCustomer.Email = updateCustomerDTO.Email;

            await _customerRepository.UpdateAsync(existingCustomer);
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            return await _customerRepository.DeleteAsync(customerId);
        }
    }
}