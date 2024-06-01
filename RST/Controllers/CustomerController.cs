using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RST.Models.DTOs.AddDTOs;
using RST.Models.DTOs.UpdateDTOs;
using RST.Models.Services;
using RST.Models.Tables;

namespace RST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly IGenericRepository<Customer> _service;
        private readonly IMapper _mapper;
        public CustomerController(IGenericRepository<Customer> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var Customers = await _service.GetAllCustomers();
            if (Customers == null)
            {
                return StatusCode(
                    StatusCodes.Status404NotFound, "No Customers added");
            }

            return StatusCode(StatusCodes.Status200OK, Customers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var Customer = await _service.GetById(id);

            if (Customer == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, Customer);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Create(CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                if (await _service.CustomerPhoneNumberExists(customer.PhoneNumber) != -1)
                {
                    return StatusCode(StatusCodes.Status409Conflict, "Phone Number already exists");
                }
                var Customer = _mapper.Map<Customer>(customer);
                if (await _service.Add(Customer))
                {
                    return StatusCode(StatusCodes.Status201Created, await _service.GetByPhoneNumber(Customer.PhoneNumber));
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status400BadRequest,customer);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(UpdateCustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                var Customer = await _service.GetById(customer.Id);
                if (Customer == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Id not found");
                }
                if (await _service.CustomerPhoneNumberExists(Customer.PhoneNumber, Customer.Id) == 1)
                {
                    return StatusCode(StatusCodes.Status409Conflict, "Phone number already exists");
                }
                var CUSTOMER = _mapper.Map<UpdateCustomerDTO, Customer>(customer, Customer);
                if (await _service.Update(CUSTOMER))
                {
                    return StatusCode(StatusCodes.Status200OK, await _service.GetById(Customer.Id));
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return StatusCode(StatusCodes.Status400BadRequest, customer); ;
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var Customer = await _service.GetById(Id);
            if (Customer == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Id not found");
            }
            Customer.IsDeleted = true;
            if (await _service.Delete(Customer))
            {
                return StatusCode(StatusCodes.Status200OK, "Customer Deleted");
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

