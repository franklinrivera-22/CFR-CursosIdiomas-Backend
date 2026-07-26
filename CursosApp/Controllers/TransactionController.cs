using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CursosApp.Constants;
using CursosApp.Services.Transactions;

namespace CursosApp.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> GetPage(int page = 1, int pageSize = 10)
        {
            var userId = User.FindFirstValue("UserId");
            bool isAdmin = User.IsInRole(RolesConstant.ADMIN);

            var response = await _transactionService.GetPageAsync(userId, isAdmin, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = RolesConstant.ADMIN)]
        public async Task<ActionResult> GetOne(string id)
        {
            var response = await _transactionService.GetOneByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}