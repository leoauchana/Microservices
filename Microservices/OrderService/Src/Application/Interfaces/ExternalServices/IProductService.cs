using Application.DTOs;
using Domain.Contracts;

namespace Application.Interfaces.ExternalServices;

public interface IProductService
{
    Task<ProductSnapshot?> GetById(Guid productId);
    Task<List<ProductSnapshot>?> GetAllById(List<Guid> productIds);
    // Task ReduceStock(Dictionary<Guid, int> reduceProductStock);
    Task ReduceStock(OrderCreatedEvent orderCreated);
}
