using Microsoft.EntityFrameworkCore;
using MiniBank.Core.Data;
using MiniBank.Core.Entities.Account;
using MiniBank.Core.Repositories.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Repository.Implementation.Account
{
    public class CustomerAccountRepository : GenericRepository<CustomerAccount>, ICustomerAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerAccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CustomerAccount> GetCustomerAccountByEmail(string email)
        {
            return await _context.CustomerAccounts.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
