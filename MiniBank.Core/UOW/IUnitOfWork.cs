using MiniBank.Core.Repositories.Account;
using MiniBank.Core.Repositories.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBank.Core.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerAccountRepository CustomerAccounts { get; }
        ICustomerRepository Customers { get; }
        

        Task CompleteAsync();
        void Complete();
        //Task CompleteAsync();
        ///// <summary>
        ///// This will be rarely needed for saving to the DB synchronously
        ///// </summary>
        //void Complete();
    }
}
