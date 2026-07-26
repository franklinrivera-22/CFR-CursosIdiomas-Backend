using CursosApp.Constants;
using CursosApp.Database;
using CursosApp.Dtos.Common;
using CursosApp.Dtos.Transactions;
using CursosApp.Entities;
using CursosApp.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CursosApp.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public TransactionService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }
        public async Task<ResponseDto<PageDto<List<TransactionDto>>>> GetPageAsync(
            string userId, bool isAdmin, int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);
            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<TransactionEntity> query = _context.Transactions
                .Include(t => t.Items);


            if (!isAdmin)
            {
                query = query.Where(t => t.UserId == userId);
            }

            int totalRows = await query.CountAsync();

            var entities = await query
                .OrderByDescending(t => t.CreatedDate) 
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            return new ResponseDto<PageDto<List<TransactionDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_FOUND,
                Data = new PageDto<List<TransactionDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = TransactionMapper.ListEntityToListDto(entities),
                    HasNextPage = page < (int)Math.Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<TransactionDto>> GetOneByIdAsync(string id)
        {
            var entity = await _context.Transactions
                .Include(t => t.Items)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity is null)
            {
                return new ResponseDto<TransactionDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND
                };
            }

            return new ResponseDto<TransactionDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Data = TransactionMapper.EntityToDto(entity)
            };
        }
    }
}