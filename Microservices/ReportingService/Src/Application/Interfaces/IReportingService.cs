using Application.ReadModels;

namespace Application.Interfaces;

public interface IReportingService
{
    Task<bool> RegisterOrderCreated(Guid idOrder,
                                    decimal total,
                                    DateOnly creationDate,
                                    Dictionary<Guid, int> productStock);
    Task<bool> RegisterProductCreated(Guid idProduct,
                                      string name,
                                      string description,
                                      DateOnly creationDate);
    Task<List<OrderReport>> GetOrdersByDate(int limit = 10,
                                            DateOnly? date = null);
}
