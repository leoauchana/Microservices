using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Messaging.RabbitMq;
using Application.Messaging.RabbitMq.Events;
using Application.Messaging.RabbitMq.Interfaces;
using Application.Utilities;
using Domain.Entities;
using Domain.Interfaces;

namespace Application;

public class ProductService : IProductService
{
    private readonly IRepository _repository;
    private readonly IMessagePublisher _messagePublisher;
    public ProductService(IRepository repository, IMessagePublisher messagePublisher)
    {
        _repository = repository;
        _messagePublisher = messagePublisher;
    }

    // Services for the API

    public async Task<bool> Create(ProductDto.Request newProduct)
    {
        var productFound = await _repository.GetTheFirstOne<Product>(p => p.Name.Equals(newProduct.name));

        if (productFound != null)
            throw new BusinessConflictException("A product with the same name already exists.");

        var product = new Product(newProduct.name, newProduct.description, newProduct.price, newProduct.stock);

        await _repository.Add(product);

        var productCreatedEvent = new ProductCreatedEvent(product.Id, product.Name, product.Price, product.Description);    

        await _messagePublisher.PublishMessage(
            RabbitMqTopics.ProductsExchange,
            RabbitMqTopics.ProductCreatedRoutingKey,
            productCreatedEvent);

        return true;
    }

    public async Task<List<ProductDto.Response>> GetAll()
    {
        var products = await _repository.GetAll<Product>();

        if (!products.Any()) return new List<ProductDto.Response>();

        return products.Select(p =>
            new ProductDto.Response
            (p.Id, p.Name, p.Description, p.Price, p.Stock))
            .ToList();
    }

    public async Task<ProductDto.Response> GetById(string id)
    {
        var idValidated = id.ValidateId();

        var product = await _repository.GetForId<Product>(idValidated);

        if (product == null)
            throw new EntityNotFoundEception("Product not found.");

        return new ProductDto.Response
            (product.Id, product.Name, product.Description,
            product.Price, product.Stock);
    }

    // Services for the order service

    public async Task<List<ProductDto.Response>> GetByIds(List<string> productIds)
    {
        var productList = new List<Product>();
        foreach (var id in productIds)
        {
            var idProduct = id.ValidateId();

            var productFound = await _repository.GetForId<Product>(idProduct);
            if(productFound == null)
                throw new EntityNotFoundEception($"The product with id {id} not found");
            productList.Add(productFound);
        }
        return productList.Select(p =>
            new ProductDto.Response
            (p.Id, p.Name, p.Description, p.Price, p.Stock))
            .ToList();
    }

    public async Task<bool> ReduceStock(List<ProductDto.Stock> productStock)
    {
        foreach (var productInf in productStock)
        {
            if (productInf.quantity <= 0)
                throw new FormatInvalidException("The format the quantity is invalid");

            var idProduct = productInf.idProduct.ValidateId();

            var productFound = await _repository.GetForId<Product>(idProduct);

            if (productFound == null)
                throw new EntityNotFoundEception("The product not found");

            productFound.ReduceStock(productInf.quantity);

            await _repository.Update(productFound);
        }

        return true;
    }
}
