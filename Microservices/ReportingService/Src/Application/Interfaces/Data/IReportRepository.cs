using Application.ReadModels;

namespace Application.Interfaces.Repositories;

public interface IReportRepository
{
    Task RegisterOrderCreated(Guid idOrder, 
                                    decimal total, 
                                    DateOnly creationDate,
                                    Dictionary<Guid, int> productStock);
    Task<List<OrderReport>> GetOrdersByDate(int limit = 10, 
                                            DateOnly? date = null);
}
