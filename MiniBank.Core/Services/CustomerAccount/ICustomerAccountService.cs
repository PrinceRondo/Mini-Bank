using MiniBank.Core.DTOs;
using MiniBank.Core.DTOs.RequestDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Core.Services.CustomerAccount
{
    public interface ICustomerAccountService
    {
        Task<GenericResponseModel> CreditAccount(string email, double amount);
        Task<GenericResponseModel> DebitAccount(DebitDto debitDto);
    }
}
