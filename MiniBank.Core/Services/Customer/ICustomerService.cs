using MiniBank.Core.DTOs;
using MiniBank.Core.DTOs.RequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Core.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<GenericResponseModel> AddCustomer(CustomerDto customer);
    }
}
