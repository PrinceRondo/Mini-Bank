using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniBank.Core.Repositories.Account;
using MiniBank.Core.Repositories.Customers;
using MiniBank.Core.Services.CustomerAccount;
using MiniBank.Core.Services.CustomerService;
using MiniBank.Core.UOW;
using MiniBank.Repository.Implementation.Account;
using MiniBank.Repository.Implementation.Customers;
using MiniBank.Repository.UOW;
using MiniBank.Service.Account;
using MiniBank.Service.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            //Service
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerAccountService, CustomerAccountService>();
            //Repo
            services.AddTransient<ICustomerAccountRepository, CustomerAccountRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
