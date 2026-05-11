using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto.Request request)
    {
        var result = await _productService.Create(request);

        if (!result) return BadRequest("Failed to create product");

        return Ok("Product created successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAll();
        return Ok(new { products = products, count = products.Count });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var product = await _productService.GetById(id);

        if (product == null) return BadRequest("Error getting product");

        return Ok(new { productFound = product });
    }

    // Endpoints for microservices communication

    [HttpPost("getByIds")]
    public async Task<IActionResult> GetByIds([FromBody] List<string> ids)
    {
        var products = await _productService.GetByIds(ids);

        if (products == null) return BadRequest("Error getting products");

        return Ok(new { productsFound = products, count = products.Count });
    }

    [HttpPatch("reduceStock")]
    public async Task<IActionResult> ReduceStock([FromBody] Dictionary<Guid, int> productStock)
    {
        var reduceProductStock = productStock.Select(ps => new ProductDto.Stock(ps.Key.ToString(), ps.Value)).ToList();

        var reduceStock = await _productService.ReduceStock(reduceProductStock);

        if (!reduceStock) return BadRequest("Error reducing stock");

        return Ok("Reduce stock successfully");
    }
}
