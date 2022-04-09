using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBank.Core.DTOs;
using MiniBank.Core.DTOs.RequestDtos;
using MiniBank.Core.Services.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBank.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("addCustomer")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> AddCustomer(CustomerDto customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _customerService.AddCustomer(customer));
        }
    }
}
