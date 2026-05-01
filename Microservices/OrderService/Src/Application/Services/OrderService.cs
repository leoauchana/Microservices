using Application.DTOs;
using Application.Interfaces;
using Application.Utilities;
using Domain.Interfaces;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IRepository _repository;
    public OrderService(IRepository repository)
    {
        _repository = repository;
    }
    public Task<bool> Create(OrderDto.Request newOrder)
    {
        var idUserValidated = newOrder.idUser.ValidateId();

        // Comunicarse con el servicio de usuario para determinar si existe el user con el id del dto

        // Validar productos con el id proveniente del dto de orderItem
        throw new NotImplementedException();
    }

    public Task<List<OrderDto.Response>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto.Response> GetById(string id)
    {
        throw new NotImplementedException();
    }
}
