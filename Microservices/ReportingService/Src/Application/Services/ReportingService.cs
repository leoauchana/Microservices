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

    public async Task<List<OrderReport>> GetOrdersByDate(
        int limit = 10,
        DateOnly? date = null)
    {
        if (!(limit > 0)) throw new FormatInvalidException("Limit has a value invalid");

        var ordersFound = await _repository.GetOrdersByDate(limit, date);

        if (!ordersFound.Any()) return new List<OrderReport>();

        return ordersFound;
    }

    public async Task<bool> RegisterOrderCreated(
        string idOrder,
        decimal total,
        DateOnly creationDate,
        Dictionary<string, int> productStock)
    {
        var idOrderValidated = idOrder.ValidateId();

        Dictionary<Guid, int> productStockValidated = new Dictionary<Guid, int>();
        foreach (var productId in productStock)
        {
            productStockValidated.Add(productId.Key.ValidateId(), productId.Value);
        }
        if (total <= 0)
            throw new FormatInvalidException("Total must be greater than zero.");
        await _repository.RegisterOrderCreated(
                                    idOrderValidated,
                                    total,
                                    creationDate,
                                    productStockValidated);
        return true;
    }
}
