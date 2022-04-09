using MiniBank.Core.Entities.Customers;
using MiniBank.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.Core.Entities.Account
{
    public class CustomerAccount : BaseEntity
    {
        public string Email { get; set; }
        public double Balance { get; set; }

        //[ForeignKey("CustomerId")]
        //public virtual Customer Customer { get; set; }
    }
}
