using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportingService _reportingService;
    public ReportController(IReportingService reportingService)
    {
        _reportingService = reportingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersByDate(
        [FromQuery] int limit = 10,
        [FromQuery] DateOnly? date = null)
    {
        var orders = await _reportingService.GetOrdersByDate(limit, date);

        return Ok(new { ordersFound = orders, count = orders.Count });
    }
}
