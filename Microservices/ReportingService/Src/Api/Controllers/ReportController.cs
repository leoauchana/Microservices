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

    [HttpGet("getOrders")]
    public async Task<IActionResult> GetOrdersByDate(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] DateOnly? from = null,
        [FromQuery] DateOnly? to = null)
    {
        var orders = await _reportingService.GetOrdersByDate(page, pageSize, from, to);

        return Ok(new { ordersFound = orders, count = orders.Count });
    }
    [HttpGet("getProducts")]
    public async Task<IActionResult> GetProductsMoreSales(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] DateOnly? date = null)
    {
        var products = await _reportingService.GetProductsMoreSales(page, pageSize, date);

        return Ok(new { productsFound = products, count = products.Count });
    }
}
