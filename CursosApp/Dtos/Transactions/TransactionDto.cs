using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosApp.Dtos.Transactions
{
    public class TransactionDto
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string PaymentReference { get; set; }
        public string PaymentMessage { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<TransactionItemDto> Items { get; set; }
    }
}