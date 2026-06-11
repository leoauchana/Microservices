using Application.DTOs;
using Application.ReadModels;

namespace Application.Interfaces;

public interface IReportingService
{
    Task<bool> RegisterOrderCreated(Guid idOrder,
                                    decimal total,
                                    DateTime creationDate,
                                    Dictionary<Guid, int> productStock);
    Task<bool> RegisterProductCreated(Guid idProduct,
                                      string name,
                                      string description,
                                      DateTime creationDate);
    Task<List<OrderDto.GetOrderByDateResponse>> GetOrdersByDate(int page = 1,
                                            int pageSize = 50,
                                            DateOnly? date = null);
    Task<List<ProductDto.GetProductsMoreSalesResponse>> GetProductsMoreSales(int page = 1,
                                                                            int pageSize = 10,
                                                                            DateOnly? date = null);
}
