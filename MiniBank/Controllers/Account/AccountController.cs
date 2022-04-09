using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBank.Core.DTOs;
using MiniBank.Core.DTOs.RequestDtos;
using MiniBank.Core.Services.CustomerAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBank.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICustomerAccountService _customerAccountService;

        public AccountController(ICustomerAccountService customerAccountService)
        {
            _customerAccountService = customerAccountService;
        }

        [HttpPut]
        [Route("creditAccount")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> CreditAccount(string email, double amount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _customerAccountService.CreditAccount(email, amount));
        }

        [HttpPut]
        [Route("debitAccount")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> DebitAccount(DebitDto debitDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _customerAccountService.DebitAccount(debitDto));
        }
    }
}
