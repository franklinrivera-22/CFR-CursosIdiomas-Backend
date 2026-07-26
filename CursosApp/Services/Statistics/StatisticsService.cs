using CursosApp.Constants;
using CursosApp.Database;
using CursosApp.Dtos.Common;
using CursosApp.Dtos.Statistics;
using Microsoft.EntityFrameworkCore;


namespace CursosApp.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly AppDbContext _context;

        public StatisticsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<StatisticsDto>> GetCountsAsync()
        {
            var stats = new StatisticsDto
            {
                CoursesCount = await _context.Courses.CountAsync(),
                CategoriesCount = await _context.Categories.CountAsync(),
                TransactionsCount = await _context.Transactions.CountAsync(),
                TotalRevenue = await _context.Transactions
                    .Where(t => t.Status == TransactionStatus.COMPLETED)
                    .SumAsync(t => (decimal?)t.Amount) ?? 0m
            };

            return new ResponseDto<StatisticsDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Data = stats
            };
        }


    }
}