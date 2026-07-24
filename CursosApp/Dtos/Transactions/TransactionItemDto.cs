using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosApp.Dtos.Transactions
{
    public class TransactionItemDto
    {
        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}