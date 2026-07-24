using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosApp.Dtos.Payments
{
    public class PaymentResultDto
    {
        public bool Approved { get; set; }
        public string Reference { get; set; } 
        public string Message { get; set; }  
    }
}