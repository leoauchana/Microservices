using System.Text;
using System.Text.Json;
using Application.DTOs;
using Application.Interfaces;
using Infraestructure.Messaging.RabbitMq.Connections;
using Infraestructure.Messaging.RabbitMq.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infraestructure.Messaging.RabbitMq.Consumers;

public class OrderCreatedConsumer : BackgroundService
{
    private readonly RabbitMqConnection _rabbitMqConnection;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<OrderCreatedConsumer> _logger;
    public OrderCreatedConsumer(
      RabbitMqConnection rabbitMqConnection
    , IServiceScopeFactory scopeFactory
    , ILogger<OrderCreatedConsumer> logger)
    {
        _rabbitMqConnection = rabbitMqConnection;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        var connection = await _rabbitMqConnection.CreateConnectionAsync();
        
        using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await channel.ExchangeDeclareAsync(
            exchange: RabbitMqTopics.OrdersExchange,
            type: ExchangeType.Topic,
            durable: true,
            cancellationToken: stoppingToken
        );

        await channel.QueueDeclareAsync(
            queue: "products.stock.queue",
            durable: true,
            exclusive: true,
            autoDelete: false,
            cancellationToken: stoppingToken
        );

        await channel.QueueBindAsync(
            exchange: RabbitMqTopics.OrdersExchange,
            queue: "products.stock.queue",
            routingKey: RabbitMqTopics.OrderCreatedRoutingKey,
            cancellationToken: stoppingToken
        );

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, eventArgs) =>
        {
            try
            {
            var body = eventArgs.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);

            var orderCreatedEvent = JsonSerializer
                                        .Deserialize
                                        <OrderCreatedEvent>(message);

            if(orderCreatedEvent == null) 
                return;
            
            using var scope = _scopeFactory
                                    .CreateScope();

            var productService = scope.ServiceProvider
                                        .GetRequiredService
                                        <IProductService>();
                                        
            var reduceProductStock = orderCreatedEvent
                                        .productStock
                                        .Select(ps => 
                                        new ProductDto.Stock
                                        (ps.Key.ToString(), 
                                        ps.Value))
                                        .ToList();
            
            await productService.ReduceStock(reduceProductStock);

            await channel.BasicAckAsync(
                eventArgs.DeliveryTag,
                multiple: false,
                cancellationToken: stoppingToken
            );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                await channel.BasicNackAsync(
                    eventArgs.DeliveryTag,
                    multiple: false,
                    requeue: true,
                    cancellationToken: stoppingToken
                );
            }
        };

        await channel.BasicConsumeAsync(
            queue: "products.stock.queue",
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken
        );

        await Task.Delay(
            Timeout.Infinite,
            stoppingToken
        );
    }
}
