using Application.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<bool> CreateProduct(ProductDto.Request newProduct);
    Task<ProductDto.Response> GetProductById(string id);
    Task<List<ProductDto.Response>> GetAllProducts();
}
