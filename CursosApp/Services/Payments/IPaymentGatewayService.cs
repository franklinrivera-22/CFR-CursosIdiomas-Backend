using CursosApp.Dtos.Payments;

namespace CursosApp.Services.Payments
{

    public interface IPaymentGatewayService
    {
        Task<PaymentResultDto> ProcessPaymentAsync(PaymentRequestDto request);
    }
}