using Application.DTOs;

namespace Application.Interfaces;

public interface IProductService
{
    Task<bool> Create(ProductDto.Request newProduct);
    Task<ProductDto.Response> GetById(string id);
    Task<List<ProductDto.Response>> GetAll();
    Task<bool> ReduceStock(string idProduct, int quantity);
}
