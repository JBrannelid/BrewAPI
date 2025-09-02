using BrewAPI.DTOs.Customers;
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

        // Retrieve all customers from Db and map to DTOs
        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            var customerDTOs = customers.Select(c => new CustomerDTO
            {
                CustomerId = c.PK_CustomerId,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email
            }).ToList();

            // returns list of CustomerDTO
            return customerDTOs;
        }

        // Retrieves a single customer by Id, returns null if not found
        public async Task<CustomerDTO?> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
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

        // Creates a new customer in the database and returns the new Id (primary key)
        public async Task<int> CreateCustomerAsync(CreateCustomerDTO createCustomerDTO)
        {
            var customer = new Customer
            {
                Name = createCustomerDTO.Name,
                PhoneNumber = createCustomerDTO.PhoneNumber,
                Email = createCustomerDTO.Email
            };

            var newCustomerId = await _customerRepository.AddCustomerAsync(customer);
            return newCustomerId;
        }

        // Updates an existing customer. Returns true if update was successful, false if customer not found
        public async Task<bool> UpdateCustomerAsync(int customerId, UpdateCustomerDTO updateCustomerDTO)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (existingCustomer == null)
            {
                return false;
            }

            existingCustomer.Name = updateCustomerDTO.Name;
            existingCustomer.PhoneNumber = updateCustomerDTO.PhoneNumber;
            existingCustomer.Email = updateCustomerDTO.Email;

            return await _customerRepository.UpdateCustomerAsync(existingCustomer);
        }

        // Deletes a customer by Id. Returns true if deletion was successful, false if customer not found
        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            return await _customerRepository.DeleteCustomerAsync(customerId);
        }
    }
}