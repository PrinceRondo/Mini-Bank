using MiniBank.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Core.Repositories.Account
{
    public interface ICustomerAccountRepository : IGenericRepository<CustomerAccount>
    {
        Task<CustomerAccount> GetCustomerAccountByEmail(string email);
    }
}
