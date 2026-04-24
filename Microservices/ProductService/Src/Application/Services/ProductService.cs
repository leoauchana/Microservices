using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Utilities;
using Domain.Entities;
using Domain.Interfaces;

namespace Application;

public class ProductService : IProductService
{
    private readonly IRepository _repository;
    public ProductService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateProduct(ProductDto.Request newProduct)
    {
        var productFound = await _repository.GetTheFirstOne<Product>(p => p.Name.Equals(newProduct.name));

        if (productFound != null)
            throw new BusinessConflictException("A product with the same name already exists.");

        var product = new Product(newProduct.name, newProduct.description, newProduct.price, newProduct.stock);

        await _repository.Add(product);

        return true;
    }

    public async Task<List<ProductDto.Response>> GetAllProducts()
    {
        var products = await _repository.GetAll<Product>();

        if (!products.Any()) return [];

        return products.Select(p =>
            new ProductDto.Response
            (p.Id, p.Name, p.Description, p.Price, p.Stock))
            .ToList();
    }

    public async Task<ProductDto.Response> GetProductById(string id)
    {
        var idValidated = id.ValidateId();

        var product = await _repository.GetForId<Product>(idValidated);

        if (product == null)
            throw new EntityNotFoundEception("Product not found.");

        return new ProductDto.Response
            (product.Id, product.Name, product.Description,
            product.Price, product.Stock);
    }
}
