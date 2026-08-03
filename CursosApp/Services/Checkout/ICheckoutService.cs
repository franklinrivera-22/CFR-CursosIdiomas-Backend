using CursosApp.Dtos.Checkout;
using CursosApp.Dtos.Common;
using CursosApp.Dtos.Transactions;

namespace CursosApp.Services.Checkout
{
    public interface ICheckoutService
    {
        Task<ResponseDto<CreateOrderResponseDto>> CreateOrderAsync(
            CheckoutRequestDto dto, string userId);

        Task<ResponseDto<CheckoutResponseDto>> ConfirmOrderAsync(
            string orderId, string userId);
    }
}