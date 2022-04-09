using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Core.DTOs.RequestDtos
{
    public class DebitDto
    {
        public string Email { get; set; }
        public double Amount { get; set; }
        public string BeneficiaryAccountName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string BeneficiaryBankCode { get; set; }
    }
}
