using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosApp.Dtos.Payments
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiry { get; set; }
        public string CardCvv { get; set; }
        public string CustomerEmail { get; set; }
    }
}