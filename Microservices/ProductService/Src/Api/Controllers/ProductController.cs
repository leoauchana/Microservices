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
        var result = await _productService.CreateProduct(request);

        if (!result) return BadRequest("Failed to create product");

        return Ok("Product created successfully");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProducts();
        return Ok(new { products = products, count = products.Count });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var product = await _productService.GetProductById(id);

        if (product == null) return BadRequest("Error getting product");

        return Ok(product);
    }

}
