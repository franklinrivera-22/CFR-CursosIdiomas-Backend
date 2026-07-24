using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosApp.Dtos.Transactions
{
    public class CheckoutResponseDto
    {
        public bool Approved { get; set; }
        public string PaymentReference { get; set; }
        public TransactionDto Transaction { get; set; }
    }
}