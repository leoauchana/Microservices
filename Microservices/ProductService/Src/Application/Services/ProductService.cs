using Application.Interfaces;
using Domain.Interfaces;

namespace Application;

public class ProductService : IProductService
{
    private readonly IRepository _repository;
    public ProductService(IRepository repository)
    {
        _repository = repository;
    }
}
