using MiniBank.Core.Data;
using MiniBank.Core.Repositories.Account;
using MiniBank.Core.Repositories.Customers;
using MiniBank.Core.UOW;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICustomerAccountRepository CustomerAccounts { get; }
        public ICustomerRepository Customers { get; }

        public UnitOfWork(ApplicationDbContext dbContext,
            ICustomerAccountRepository customerAccountRepository,
            ICustomerRepository customerRepository)
        {
            _context = dbContext;

            CustomerAccounts = customerAccountRepository;
            Customers = customerRepository;
        }
        public async Task CompleteAsync()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (DbEntityValidationException devex)
                {
                    var outputLines = new StringBuilder();

                    foreach (var eve in devex.EntityValidationErrors)
                    {
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.AppendLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }

                    transaction.Rollback();

                    throw new DbEntityValidationException(outputLines.ToString(), devex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void Complete()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //AuditLogs.SetUpAuditTrail();
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (DbEntityValidationException devex)
                {
                    var outputLines = new StringBuilder();

                    foreach (var eve in devex.EntityValidationErrors)
                    {
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.AppendLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }

                    transaction.Rollback();

                    throw new DbEntityValidationException(outputLines.ToString(), devex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
