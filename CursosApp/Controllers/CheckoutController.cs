using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CursosApp.Constants;
using CursosApp.Dtos.Checkout;
using CursosApp.Services.Checkout;

namespace CursosApp.Controllers
{
    [ApiController]
    [Route("api/checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("create-order")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RolesConstant.NORMAL_USER)]
        public async Task<ActionResult> CreateOrder([FromBody] CheckoutRequestDto dto)
        {
            var userId = User.FindFirstValue("UserId");
            var response = await _checkoutService.CreateOrderAsync(dto, userId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("capture")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RolesConstant.NORMAL_USER)]
        public async Task<ActionResult> Capture([FromBody] ConfirmOrderDto dto)
        {
            var userId = User.FindFirstValue("UserId");
            var response = await _checkoutService.ConfirmOrderAsync(dto.OrderId, userId);
            return StatusCode(response.StatusCode, response);
        }
    }
}