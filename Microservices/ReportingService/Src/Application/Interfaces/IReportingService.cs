using Application.ReadModels;

namespace Application.Interfaces;

public interface IReportingService
{
    Task<bool> RegisterOrderCreated(string idOrder, 
                                    decimal total, 
                                    DateOnly creationDate,
                                    Dictionary<string, int> productStock); 
    Task<List<OrderReport>> GetOrdersByDate(int limit = 10, 
                                            DateOnly? date = null);
}
