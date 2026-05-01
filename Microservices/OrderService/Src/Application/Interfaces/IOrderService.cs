using Application.DTOs;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<bool> Create(OrderDto.Request newOrder);
    Task<OrderDto.Response> GetById(string id);
    Task<List<OrderDto.Response>> GetAll();
}
