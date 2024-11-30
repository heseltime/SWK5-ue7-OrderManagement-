using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.Dtos;
using OrderManagement.Api.Mapping;
using OrderManagement.Api.Mapper;
using OrderManagement.Domain;
using OrderManagement.Logic;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IOrderManagementLogic logic;

        public CustomersController(IOrderManagementLogic orderManagementLogic)
        {
            this.logic = orderManagementLogic ??
                throw new NullReferenceException(nameof(orderManagementLogic));
        }

        // GET /api/Customers
        // GET /api/Customers?rating=A
        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetCustomers([FromQuery] Rating? rating)
        {
            if (rating is null)
            {
                return (await logic.GetCustomersAsync()).Select(c => c.ToCustomerDto());
            }
            else
            {
                return (await logic.GetCustomersByRatingAsync(rating.Value))
                    .Select(c => c.ToCustomerDto());
            }
        }

        // GET /api/Customers/<GUID>
        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById([FromRoute] Guid customerId)
        {
            var customer = await logic.GetCustomerAsync(customerId);

            if (customer is null) {
                return NotFound();
            }

            return Ok(customer.ToCustomerDto());
        }

        // POST /api/Customers (customer also enable now in Program.cs)
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CustomerForCreationDto customerDto)
        {
            if (customerDto.Id != Guid.Empty &&
                await logic.CustomerExistsAsync(customerDto.Id))
            {
                return Conflict();
            }

            Domain.Customer customer = customerDto.ToCustomer();
            await logic.AddCustomerAsync(customer);

            return CreatedAtAction(actionName: nameof(GetCustomerById),
                routeValues: new { customerId = customer.Id },
                value: customer.ToCustomerDto());
        }

        // UpdateCustomer
        // PUT /api/customers/<GUID>
        [HttpPut("{customerId}")]
        public async Task<ActionResult> UpdateCustomer([FromRoute] Guid customerId, [FromBody] CustomerForUpdateDto customerForUpdateDto)
        {
            Domain.Customer? customer = await logic.GetCustomerAsync(customerId);
            if (customer is null)
            {
                return NotFound();
            }

            customerForUpdateDto.UpdateCustomer(customer);
            await logic.UpdateCustomerAsync(customer);
            return NoContent();
        }

        // Delete
        // DELETE /api/customers/<GUID>
        [HttpDelete("{customerId}")]
        public async Task<ActionResult> DeleteCustomer(Guid customerId)
        {
            if (await logic.DeleteCustomerAsync(customerId))
            {
                return NoContent(); // design decision to let client know if succesful or not
            } else
            {
                return NoContent();
            }
        }

    }
}
