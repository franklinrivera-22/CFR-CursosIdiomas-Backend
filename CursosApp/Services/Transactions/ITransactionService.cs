using CursosApp.Dtos.Common;
using CursosApp.Dtos.Transactions;

namespace CursosApp.Services.Transactions
{
    public interface ITransactionService
    {
        Task<ResponseDto<PageDto<List<TransactionDto>>>> GetPageAsync(
            string userId, bool isAdmin, int page = 1, int pageSize = 10);
        Task<ResponseDto<TransactionDto>> GetOneByIdAsync(string id);
    }
}