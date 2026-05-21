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
    public async Task<IActionResult> Create([FromBody] OrderDto.Create newOrder)
    {
        var orderRegistered = await _orederService.Create(newOrder);
        if (!orderRegistered) return BadRequest("Order could not be created.");
        return Ok("Order created successfully.");
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orederService.GetAll();
        return Ok(new { orders, count = orders.Count });
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string orderId)
    {
        var orderFound = await _orederService.GetById(orderId);
        if (orderFound == null) return NotFound("Order not found.");
        return Ok(new { orderFound });
    }
}