using Application.Common;
using Application.ReadModels;

namespace Application.Interfaces.Repositories;

public interface IReportRepository
{
    Task RegisterOrderCreated(Guid idOrder,
                                    decimal total,
                                    DateTime creationDate,
                                    Dictionary<Guid, int> productStock);
    Task<PagedResult<OrderReport>> GetOrdersByDate(int page,
                                            int pageSize,
                                            DateOnly? from = null,
                                            DateOnly? to = null);
    Task<PagedResult<ProductReport>> GetProductsMoreSalesByDate(int page,
                                            int pageSize,
                                            DateOnly? from = null,
                                            DateOnly? to = null);
    Task RegisterProductCreated(Guid idProduct,
                                string name,
                                string description,
                                DateTime creationDate);
}
