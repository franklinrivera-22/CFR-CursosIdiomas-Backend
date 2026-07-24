
using CursosApp.Dtos.Transactions;
using CursosApp.Entities;

namespace CursosApp.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionDto EntityToDto(TransactionEntity entity)
        {
            return new TransactionDto
            {
                Id = entity.Id,
                CustomerName = entity.CustomerName,
                CustomerEmail = entity.CustomerEmail,
                Amount = entity.Amount,
                Status = entity.Status,
                PaymentReference = entity.PaymentReference,
                PaymentMessage = entity.PaymentMessage,
                CreatedDate = entity.CreatedDate,
                Items = entity.Items == null ? new List<TransactionItemDto>() :
                    entity.Items.Select(i => new TransactionItemDto
                    {
                        CourseId = i.CourseId,
                        CourseTitle = i.CourseTitle,
                        UnitPrice = i.UnitPrice,
                        Quantity = i.Quantity
                    }).ToList()
            };
        }

        public static List<TransactionDto> ListEntityToListDto(List<TransactionEntity> entities)
        {
            return entities.Select(EntityToDto).ToList();
        }
    }
}