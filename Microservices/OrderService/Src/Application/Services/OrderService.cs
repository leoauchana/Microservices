using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.ExternalServices;
using Application.Utilities;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IRepository _repository;
    // Reference to the product service
    private readonly IProductService _productService;
    // Reference to the user service
    private readonly IUserService _userService;
    public OrderService(IRepository repository
        , IUserService userService
        , IProductService productService)
    {
        _repository = repository;
        _userService = userService;
        _productService = productService;
    }
    public async Task<bool> Create(OrderDto.Request newOrder)
    {
        var idUserValidated = newOrder.idUser.ValidateId();

        // Comunicarse con el servicio de usuario para determinar si existe el user con el id del dto

        var existsUser = await _userService.Validate(idUserValidated);

        if (!existsUser)
            throw new EntityNotFoundException("The user does not exist");

        // Validar productos con el id proveniente del dto de orderItem

        var productIds = newOrder.orderItems.Select(oi => oi.idProduct.ValidateId()).ToList();

        var listProducts = await _productService.GetAllById(productIds);

        if (!(listProducts!.Count == productIds.Count))
            throw new EntityNotFoundException("Some products do not exist");

        var orderCreated = new Order(idUserValidated);

        var orderItemsByProduct = newOrder.orderItems
                                        .ToDictionary(
                                        x => x.idProduct.ValidateId(),
                                        x => x.quantity);

        foreach (var pr in listProducts)
        {
            var quantity = orderItemsByProduct[pr.id.ValidateId()];

            if (pr.stock <= 0 || pr.stock < quantity)
                throw new BusinessConflictException($"Insufficient stock for product {pr.name}");

            orderCreated.AddOrderItem(new OrderItem(pr.id.ValidateId(), quantity, pr.price));
        }

        await _repository.Add(orderCreated);

        return true;
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
