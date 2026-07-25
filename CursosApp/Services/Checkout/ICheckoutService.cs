using CursosApp.Dtos.Checkout;
using CursosApp.Dtos.Common;
using CursosApp.Dtos.Transactions;


namespace CursosApp.Services.Checkout
{
    public interface ICheckoutService
    {
        Task<ResponseDto<CheckoutResponseDto>> ProcessCheckoutAsync(
            CheckoutRequestDto dto, string userId);
    }
}