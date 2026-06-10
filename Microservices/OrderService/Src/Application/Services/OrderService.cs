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
    // Reference to the message publisher
    private readonly IMessagePublisher _messagePublisher;
    public OrderService(IRepository repository
        , IUserService userService
        , IProductService productService
        , IMessagePublisher messagePublisher)
    {
        _repository = repository;
        _userService = userService;
        _productService = productService;
        _messagePublisher = messagePublisher;
    }
    public async Task<bool> Create(OrderDto.Create newOrder)
    {
        var idUserValidated = newOrder.idUser.ValidateId();

        // Comunicarse con el servicio de usuario para determinar si existe el user con el id del dto

        var existsUser = await _userService.Validate(idUserValidated);

        if (!existsUser)
            throw new EntityNotFoundException("The user does not exist");

        // Validate products with the id of order item from dto

        var productIds = newOrder.orderItems.Select(oi => oi.idProduct.ValidateId()).ToList();

        // Get products for the id

        var listProducts = await _productService.GetAllById(productIds);

        if (!(listProducts!.Count == productIds.Count))
            throw new EntityNotFoundException("Some products do not exist");
        
        // Order created 

        var orderCreated = new Order(idUserValidated);

        // Map to dictionary with product id and stock quantity to reduce

        var orderItemsByProduct = newOrder.orderItems
                                        .ToDictionary(
                                        x => x.idProduct.ValidateId(),
                                        x => x.quantity);

        // Add order item to order created

        foreach (var pr in listProducts)
        {
            var quantity = orderItemsByProduct[pr.id.ValidateId()];

            if (pr.stock <= 0 || pr.stock < quantity)
                throw new BusinessConflictException($"Insufficient stock for product {pr.name}");

            orderCreated.AddOrderItem(new OrderItem(pr.id.ValidateId(), quantity, pr.price));
        }

        // Order saved

        await _repository.Add(orderCreated);

        // Message publicated 

        await _messagePublisher.PublishMessage(
            RabbitMqTopics.OrdersExchange,
            RabbitMqTopics.OrderCreatedRoutingKey,
            new OrderCreatedEvent(
                orderCreated.Id,
                (decimal)orderCreated.Total,
                orderCreated.Date,
                orderItemsByProduct)
        );
        // Se comento la llamada al microservicio de producto con comunicacion sincrona
        // await _productService.ReduceStock(orderItemsByProduct);

        return true;
    }

    public async Task<List<OrderDto.GetAll>> GetAll()
    {
        var orderList = await _repository.ListAll<Order>(nameof(Order.OrderItems));

        if (!orderList.Any())
            return new List<OrderDto.GetAll>();

        var dtoList = new List<OrderDto.GetAll>();

        foreach (var order in orderList)
        {
            var userFound = await _userService.GetById(order.UserId);
            if (userFound == null)
                throw new EntityNotFoundException($"The user with id {order.UserId} associated to order with id {order.Id} does not exist");

            dtoList.Add(new OrderDto.GetAll(
                order.Id.ToString(), 
                new UserDto.Response(userFound.fullName, userFound.email),
                order.OrderItems.Count,
                order.Total,
                order.Date));
        }

        return dtoList;
    }

    public async Task<OrderDto.GetById> GetById(string id)
    {
        var idValidated = id.ValidateId();

        var orderFound = await _repository.GetForId<Order>(idValidated, nameof(OrderItem));

        if (orderFound == null)
            throw new EntityNotFoundException("The order does not exist");

        var userFound = await _userService.GetById(orderFound.UserId);

        if (userFound == null)
            throw new EntityNotFoundException("The user does not exist");

        var productsId = orderFound.OrderItems.Select(oi => oi.ProductId).ToList();

        var listProducts = await _productService.GetAllById(productsId);

        if (!(listProducts!.Count == productsId.Count))
            throw new EntityNotFoundException("Some products do not exist");

        var listOrderItems = orderFound.OrderItems.Select(oi =>
        {
            var product = listProducts.First(p => p.id.ValidateId() == oi.ProductId);
            return new OrderItemDto.Response(new ProductDto.Response(product.name, product.price), oi.Quantity, oi.SubTotal);
        }).ToList();

        return new OrderDto.GetById(orderFound.Id.ToString(),
            new UserDto.Response(userFound.fullName, userFound.email),
            listOrderItems, orderFound.Total, orderFound.Date);
    }
}