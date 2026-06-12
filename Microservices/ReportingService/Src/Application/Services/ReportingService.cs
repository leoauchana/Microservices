using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.ReadModels;
using Application.Utilities;

namespace Application.Services;

public class ReportingService : IReportingService
{
    private readonly IReportRepository _repository;
    public ReportingService(IReportRepository repository)
    {
        _repository = repository;
    }

    // Methods for api


    // TODO: Fix and define dtos for this method
    public async Task<List<OrderDto.GetOrderByDateResponse>> GetOrdersByDate(
        int page = 1,
        int pageSize = 10,
        DateOnly? from = null,
        DateOnly? to = null)
    {
        if (!(page > 0) && !(pageSize > 0)) throw new FormatInvalidException("Limit has a value invalid");

        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var pagedResult = await _repository.GetOrdersByDate(page, pageSize, from, to);

        if (!pagedResult.Items.Any()) return new List<OrderDto.GetOrderByDateResponse>();

        var 

        return pagedResult..Select(o => new OrderDto.GetOrderByDateResponse())
                           .ToList();
    }

    public async Task<List<ProductDto.GetProductsMoreSalesResponse>> GetProductsMoreSales(
        int page = 1,
        int pageSize = 10,
        DateOnly? date = null
        )
    {
        throw new NotImplementedException();
    }

    // Methods for register the events of message broker

    public async Task<bool> RegisterOrderCreated(
        Guid idOrder,
        decimal total,
        DateTime creationDate,
        Dictionary<Guid, int> productStock)
    {
        Dictionary<Guid, int> productStockValidated = new Dictionary<Guid, int>();
        foreach (var productId in productStock)
        {
            productStockValidated.Add(productId.Key, productId.Value);
        }
        if (total <= 0)
            throw new FormatInvalidException("Total must be greater than zero.");
        await _repository.RegisterOrderCreated(
                                    idOrder,
                                    total,
                                    creationDate,
                                    productStockValidated);
        return true;
    }

    public async Task<bool> RegisterProductCreated(Guid idProduct,
                                                    string name,
                                                    string description,
                                                    DateTime creationDate)
    {
        await _repository.RegisterProductCreated(
                                    idProduct,
                                    name,
                                    description,
                                    creationDate);
        return true;
    }
}
