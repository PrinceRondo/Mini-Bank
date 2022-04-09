using MiniBank.Core.DTOs;
using MiniBank.Core.DTOs.RequestDtos;
using MiniBank.Core.Services.CustomerAccount;
using MiniBank.Core.UOW;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Service.Account
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly IUnitOfWork _uow;

        public CustomerAccountService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<GenericResponseModel> CreditAccount(string email, double amount)
        {
            try
            {
                //Check if account exist
                var getAccount = await _uow.CustomerAccounts.GetCustomerAccountByEmail(email);
                if (getAccount == null)
                    return new GenericResponseModel { StatusCode = 300, StatusMessage = "Account doesn't exist", Data = false };

                getAccount.Balance += amount;
                await _uow.CompleteAsync();
                return new GenericResponseModel { StatusCode = 200, StatusMessage = "Account Vredited", Data = true };
            }
            catch (Exception ex)
            {
                return new GenericResponseModel { StatusMessage = ex.Message, StatusCode = 500, Data = false };
            }
        }

        public async Task<GenericResponseModel> DebitAccount(DebitDto debitDto)
        {
            try
            {
                //Check if account exist
                var getAccount = await _uow.CustomerAccounts.GetCustomerAccountByEmail(debitDto.Email);
                if (getAccount == null)
                    return new GenericResponseModel { StatusCode = 300, StatusMessage = "Account doesn't exist", Data = false };
                if(getAccount.Balance >= debitDto.Amount)
                    return new GenericResponseModel { StatusCode = 301, StatusMessage = "Insufficient balance", Data = false };

                getAccount.Balance -= debitDto.Amount;

                var client = new RestClient("https://api.woven.finance/v2/api/payouts/request?command=initiate");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("api_secret", "vb_tk_azyweqwpwes");
                request.AddHeader("requestId", "4632b0be-9eca-411c-abd8-ca8ead61719d");
                request.AddHeader("Content-Type", "application/json");
                var body = @"{" + "\n" +
                @"    ""source_account"": ""7191836728""," + "\n" +
                @"    ""PIN"": ""0047""," + "\n" +
                @"    ""beneficiary_account_name"": "+debitDto.BeneficiaryAccountName +"," + "\n" +
                @"    ""beneficiary_nuban"": " + debitDto.BeneficiaryAccountNumber + "," + "\n" +
                @"    ""beneficiary_bank_code"": " + debitDto.BeneficiaryBankCode + "," + "\n" +
                @"    ""bank_code_scheme"": ""NIP""," + "\n" +
                @"    ""currency_code"": ""NGN""," + "\n" +
                @"    ""narration"": ""For kitchen utensils""," + "\n" +
                @"    ""amount"":"+ debitDto.Amount+"," + "\n" +
                @"    ""reference"": ""payout_20633192348268""," + "\n" +
                @"    ""callback_url"": """"" + "\n" +
                @"}";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);


                await _uow.CompleteAsync();
                return new GenericResponseModel { StatusCode = 200, StatusMessage = "Account Debited", Data = true };
            }
            catch (Exception ex)
            {
                return new GenericResponseModel { StatusMessage = ex.Message, StatusCode = 500, Data = false };
            }
        }
    }
}
