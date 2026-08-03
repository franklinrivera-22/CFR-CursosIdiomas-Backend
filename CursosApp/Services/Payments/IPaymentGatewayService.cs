using CursosApp.Dtos.Payments;

namespace CursosApp.Services.Payments
{
    public interface IPaymentGatewayService
    {
        Task<string> CreateOrderAsync(decimal amount);                       
        Task<PaymentResultDto> ProcessPaymentAsync(PaymentRequestDto request); 
    }
}