using Microsoft.EntityFrameworkCore;
using MiniBank.Core.Data;
using MiniBank.Core.Entities.Customers;
using MiniBank.Core.Repositories.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Repository.Implementation.Customers
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _context.Customers.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
