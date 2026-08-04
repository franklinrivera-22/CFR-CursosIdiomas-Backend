using CursosApp.Constants;
using CursosApp.Database;
using CursosApp.Dtos.Checkout;
using CursosApp.Dtos.Common;
using CursosApp.Dtos.Payments;
using CursosApp.Dtos.Transactions;
using CursosApp.Entities;
using CursosApp.Mappers;
using CursosApp.Services.Payments;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CursosApp.Services.Checkout
{
    public class CheckoutService : ICheckoutService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly AppDbContext _context;
        private readonly IPaymentGatewayService _paymentGateway;

        public CheckoutService(
            AppDbContext context,
            IPaymentGatewayService paymentGateway,
            UserManager<UserEntity> userManager)
        {
            _context = context;
            _paymentGateway = paymentGateway;
            _userManager = userManager;
        }

       
        public async Task<ResponseDto<CreateOrderResponseDto>> CreateOrderAsync(
            CheckoutRequestDto dto, string userId)
        {
            var courseIds = dto.Items.Select(i => i.CourseId).Distinct().ToList();

            var courses = await _context.Courses
                .Where(c => courseIds.Contains(c.Id) && c.IsActive)
                .ToListAsync();

            if (courses.Count == 0)
            {
                return new ResponseDto<CreateOrderResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Ninguno de los cursos del carrito esta disponible."
                };
            }

            var items = new List<TransactionItemEntity>();
            decimal amount = 0m;

            foreach (var cartItem in dto.Items)
            {
                var course = courses.FirstOrDefault(c => c.Id == cartItem.CourseId);
                if (course is null) continue;

                int quantity = cartItem.Quantity <= 0 ? 1 : cartItem.Quantity;
                amount += course.Price * quantity;

                items.Add(new TransactionItemEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    CourseId = course.Id,
                    CourseTitle = course.Title,
                    UnitPrice = course.Price,
                    Quantity = quantity,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                });
            }

            var user = await _userManager.FindByIdAsync(userId);

            var transaction = new TransactionEntity
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                CustomerName = $"{user.FirstName} {user.LastName}",
                CustomerEmail = user.Email,
                Amount = amount,
                Status = TransactionStatus.PENDING,
                Items = items,
                CreatedById = userId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };


            var orderId = await _paymentGateway.CreateOrderAsync(amount);
            transaction.PaymentReference = orderId;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return new ResponseDto<CreateOrderResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Orden creada.",
                Data = new CreateOrderResponseDto
                {
                    OrderId = orderId,
                    Amount = amount
                }
            };
        }

       
        public async Task<ResponseDto<CheckoutResponseDto>> ConfirmOrderAsync(
            string orderId, string userId)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Items)
                .FirstOrDefaultAsync(t => t.PaymentReference == orderId && t.UserId == userId);

            if (transaction is null)
            {
                return new ResponseDto<CheckoutResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "Orden no encontrada."
                };
            }

            var paymentResult = await _paymentGateway.ProcessPaymentAsync(new PaymentRequestDto
            {
                Amount = transaction.Amount,
                OrderId = orderId,
                CustomerEmail = transaction.CustomerEmail
            });

            transaction.Status = paymentResult.Approved
                ? TransactionStatus.COMPLETED
                : TransactionStatus.FAILED;
            transaction.PaymentReference = paymentResult.Reference;
            transaction.PaymentMessage = paymentResult.Message;
            transaction.UpdatedDate = DateTime.Now;


            if (paymentResult.Approved)
            {
                var yaInscrito = await _context.Enrollments
                    .Where(e => e.UserId == userId)
                    .Select(e => e.CourseId)
                    .ToListAsync();

                foreach (var item in transaction.Items)
                {
                    if (yaInscrito.Contains(item.CourseId)) continue;

                    _context.Enrollments.Add(new EnrollmentEntity
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = userId,
                        CourseId = item.CourseId,
                        TransactionId = transaction.Id,
                        Progress = 0,
                        IsActive = true,
                        CreatedById = userId,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    });
                    yaInscrito.Add(item.CourseId);
                }
            }



            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();

            return new ResponseDto<CheckoutResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = paymentResult.Message,
                Data = new CheckoutResponseDto
                {
                    Approved = paymentResult.Approved,
                    PaymentReference = paymentResult.Reference,
                    Transaction = TransactionMapper.EntityToDto(transaction)
                }
            };
        }
    }
}