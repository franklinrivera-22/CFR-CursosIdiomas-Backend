using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursosApp.Dtos.Statistics
{
    public class StatisticsDto
    {
        public int CoursesCount { get; set; }
        public int CategoriesCount { get; set; }
        public int TransactionsCount { get; set; }
        public decimal TotalRevenue { get; set; } 
    }
}