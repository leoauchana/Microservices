using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orederService;
    public OrderController(IOrderService orderService)
    {
        _orederService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderDto.Request newOrder)
    {
        var orderRegistered = await _orederService.Create(newOrder);
        if (!orderRegistered) return BadRequest("Order could not be created.");
        return Ok("Order created successfully.");
    }
}