using Microsoft.AspNetCore.Http;
using MiniBank.Core.DTOs;
using MiniBank.Core.DTOs.RequestDtos;
using MiniBank.Core.Entities.Account;
using MiniBank.Core.Entities.Customers;
using MiniBank.Core.Services.CustomerService;
using MiniBank.Core.UOW;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Service.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _uow;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<GenericResponseModel> AddCustomer(CustomerDto customer)
        {
            try
            {
                var getCustomer = await _uow.Customers.GetCustomerByEmail(customer.Email);
                if (getCustomer != null)
                    return new GenericResponseModel { StatusCode = 300, StatusMessage = "Customer already exist", Data = false };

                IFormFile file;
                file = customer.Image;
                //save profile picture
                string filename = Guid.NewGuid() + file.FileName.Replace(" ", "_");
                //string extension = Path.GetExtension(filename);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Image", filename);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                var imageUrl = "Image/" + filename;
                var newRecord = new Customer
                {
                    CreatedDate = DateTime.Now,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    ImageUrl = imageUrl,
                    LastName = customer.LastName,
                    ModifiedDate = DateTime.Now,
                    PhoneNumber = customer.PhoneNumber
                };
                await _uow.Customers.Add(newRecord);

                var newAccount = new CustomerAccount
                {
                    Balance = 0,
                    CreatedDate = DateTime.Now,
                    Email = customer.Email,
                    ModifiedDate = DateTime.Now
                };
                await _uow.CustomerAccounts.Add(newAccount);
                await _uow.CompleteAsync();
                return new GenericResponseModel { StatusCode = 200, StatusMessage = "Success", Data = true };

            }
            catch (Exception ex)
            {
                return new GenericResponseModel { StatusMessage = ex.Message, StatusCode = 500, Data = false };
            }
        }
    }
}
