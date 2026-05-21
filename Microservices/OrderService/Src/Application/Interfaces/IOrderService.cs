using Application.DTOs;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<bool> Create(OrderDto.Create newOrder);
    Task<OrderDto.GetById> GetById(string id);
    Task<List<OrderDto.GetAll>> GetAll();
}
  