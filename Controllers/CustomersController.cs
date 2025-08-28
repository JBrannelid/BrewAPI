using BrewAPI.DTOs;
using BrewAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        // Using dependency injection to separate business logic from the controller
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        // For admin or manager to retrieve all customers
        [HttpGet]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // GET: api/Customers/{id}
        // For admin or manager to fetch a customer by Id
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                // 404 if customer doesn’t exist
                return NotFound();
            }
            return Ok(customer);
        }

        // POST: api/Customers
        // Allows new customers to register themselves in the system
        [HttpPost]
        public async Task<ActionResult<int>> CreateCustomer(CustomerDTO customerDto)
        {
            var customerId = await _customerService.CreateCustomerAsync(customerDto);

            // 201 Created with header 
            return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, customerId);
        }

        // PUT: api/Customers/{id}
        // For admin or manager to update a customer by Id if needed
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> UpdateCustomer(int id, CustomerDTO customerDto)
        {
            var result = await _customerService.UpdateCustomerAsync(id, customerDto);
            if (!result)
            {
                // Returning 404 if trying to update a non-existing customer
                return NotFound();
            }
            // 204 - when update succeeds but no body is returned
            return NoContent();
        }

        // DELETE: api/Customers/{id}
        // For admin or manager to delete a customer record
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (!result)
            {
                // 404 if trying to delete a non-existing customer
                return NotFound();
            }
            // 204 No Content on successful deletion
            return NoContent();
        }
    }
}