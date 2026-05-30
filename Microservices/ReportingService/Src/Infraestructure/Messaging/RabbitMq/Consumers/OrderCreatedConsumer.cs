using System.Text;
using System.Text.Json;
using Application.Interfaces;
using Infraestructure.Messaging.RabbitMq.Connection;
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
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<OrderCreatedConsumer> _logger;
    public OrderCreatedConsumer(
        RabbitMqConnection rabbitMqConnection,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<OrderCreatedConsumer> logger)
    {
        _rabbitMqConnection = rabbitMqConnection;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connection = await _rabbitMqConnection.CreateConnectionAsync();

        using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await channel.ExchangeDeclareAsync(
            exchange: RabbitMqTopics.OrderExchange,
            type: ExchangeType.Topic,
            durable: true,
            cancellationToken: stoppingToken
        );

        await channel.QueueDeclareAsync(
            queue: "order.register.queue",
            durable: true,
            exclusive: true,
            autoDelete: false,
            cancellationToken: stoppingToken
        );

        await channel.QueueBindAsync(
            exchange: RabbitMqTopics.OrderExchange,
            queue: "order.register.queue",
            routingKey: RabbitMqTopics.OrderRoutingKey,
            cancellationToken: stoppingToken
        );

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, eventArgs) =>
        {
            try
            {
                var body = eventArgs.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);

                var orderCretedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

                if (orderCretedEvent == null) return;

                using var scope = _serviceScopeFactory.CreateScope();

                var _reportingService = scope.ServiceProvider.GetRequiredService<IReportingService>();

                await _reportingService.RegisterOrderCreated(
                    orderCretedEvent.idOrder,
                    orderCretedEvent.total,
                    orderCretedEvent.creationDate,
                    orderCretedEvent.productStock
                );

                await channel.BasicAckAsync(
                    eventArgs.DeliveryTag,
                    multiple: false,
                    cancellationToken: stoppingToken
                );
            }
            catch (Exception e)
            {
                _logger.LogError("Error processing message: " + e.Message);
                await channel.BasicNackAsync(
                    eventArgs.DeliveryTag,
                    multiple: false,
                    requeue: true,
                    cancellationToken: stoppingToken
                );
            }
        };
        await channel.BasicConsumeAsync(
            queue: "order.register.queue",
            autoAck: false,
            consumer,
            cancellationToken: stoppingToken
        );
        await Task.Delay(
            Timeout.Infinite,
            stoppingToken
        );
    }
}
