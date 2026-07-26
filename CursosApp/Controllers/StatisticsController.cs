using CursosApp.Services.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CursosApp.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetCounts()
        {
            var response = await _statisticsService.GetCountsAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}