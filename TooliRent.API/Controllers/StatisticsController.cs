using Microsoft.AspNetCore.Mvc;
using TooliRent.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace TooliRent.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        // Verktygsstatistik: hur ofta varje verktyg är bokat vs ledigt
        [HttpGet("tools-usage")]
        public async Task<IActionResult> GetToolsUsage(CancellationToken ct)
        {
            var data = await _statisticsService.GetToolsUsageAsync(ct);
            return Ok(data);
        }

        // Bokningsstatistik
        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookingsStats(CancellationToken ct)
        {
            var data = await _statisticsService.GetBookingStatisticsAsync(ct);
            return Ok(data);
        }

        // Användarstatistik
        [HttpGet("users")]
        public async Task<IActionResult> GetUserStats(CancellationToken ct)
        {
            var data = await _statisticsService.GetUserStatisticsAsync(ct);
            return Ok(data);
        }

        // Intäktsstatistik
        [HttpGet("revenue")]
        public async Task<IActionResult> GetRevenueStats(CancellationToken ct)
        {
            var data = await _statisticsService.GetRevenueStatisticsAsync(ct);
            return Ok(data);
        }
    }
}
