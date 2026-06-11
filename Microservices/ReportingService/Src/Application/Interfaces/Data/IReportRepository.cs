using Application.ReadModels;

namespace Application.Interfaces.Repositories;

public interface IReportRepository
{
    Task RegisterOrderCreated(Guid idOrder,
                                    decimal total,
                                    DateTime creationDate,
                                    Dictionary<Guid, int> productStock);
    Task<List<OrderReport>> GetOrdersByDate(int page,
                                            int pageSize,
                                            DateOnly? date = null);
    Task RegisterProductCreated(Guid idProduct,
                                string name,
                                string description,
                                DateTime creationDate);
}
