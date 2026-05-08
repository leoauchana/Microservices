using Application.DTOs;

namespace Application.Interfaces.ExternalServices;

public interface IProductService
{
    Task<ProductDto.Get?> GetById(Guid productId);
    Task<List<ProductDto.Get>?> GetAllById(List<Guid> productIds);
}
