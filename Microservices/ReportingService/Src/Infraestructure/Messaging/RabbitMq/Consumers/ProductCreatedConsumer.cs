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

public class ProductCreatedConsumer : BackgroundService
{
    private readonly RabbitMqConnection _rabbitMqConnection;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ProductCreatedConsumer> _logger;
    public ProductCreatedConsumer(
        RabbitMqConnection rabbitMqConnection,
        ILogger<ProductCreatedConsumer> logger,
        IServiceScopeFactory serviceScopeFactory)
    {
        _rabbitMqConnection = rabbitMqConnection;
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connection = await _rabbitMqConnection.CreateConnectionAsync();

        using var channel = await connection.CreateChannelAsync(
                                                cancellationToken: stoppingToken);
        await channel.ExchangeDeclareAsync(
            exchange: RabbitMqTopics.ProductsExchange,
            type: ExchangeType.Topic,
            durable: true,
            cancellationToken: stoppingToken
        );

        await channel.QueueDeclareAsync(
            queue: "product.register.queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: stoppingToken
        );

        await channel.QueueBindAsync(
            exchange: RabbitMqTopics.ProductsExchange,
            queue: "product.register.queue",
            routingKey: RabbitMqTopics.ProductCreatedRoutingKey,
            cancellationToken: stoppingToken
        );

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, eventArgs) =>
        {
            try
            {
                var body = eventArgs.Body.ToArray();

                var mmesage = Encoding.UTF8.GetString(body);

                var productCreatedEvent = JsonSerializer
                                            .Deserialize<ProductCreatedEvent>
                                            (mmesage);

                if (productCreatedEvent is null)
                {
                    _logger.LogError("ProductCreatedEvent is null");
                    return;
                }

                using var scope = _serviceScopeFactory.CreateScope();

                var reportingService = scope
                                        .ServiceProvider
                                        .GetRequiredService<IReportingService>();

                _logger.LogInformation(
                        "Processing ProductCreated event. CreationDate: {CreationDate}",
                        productCreatedEvent.creationDate.ToString("yyyy-MM-dd"));

                var productCreated = await reportingService.RegisterProductCreated(
                                            productCreatedEvent.id,
                                            productCreatedEvent.name,
                                            productCreatedEvent.description,
                                            productCreatedEvent.creationDate);

                if (productCreated) _logger.LogInformation("Product created registered successfully");
                else _logger.LogError("Error registering product created");

                await channel.BasicAckAsync(
                    eventArgs.DeliveryTag,
                    multiple: false,
                    cancellationToken: stoppingToken
                );
            }
            catch (Exception e)
            {
                _logger.LogError(
                    e, "Error processing message");

                await channel.BasicNackAsync(
                    eventArgs.DeliveryTag,
                    multiple: false,
                    requeue: true,
                    cancellationToken: stoppingToken
                );
            }
        };

        await channel.BasicConsumeAsync(
            queue: "product.register.queue",
            autoAck: false,
            consumer,
            cancellationToken: stoppingToken
        );

        _logger.LogInformation("ProductCreatedConsumer ready for listen to messages");

        await Task.Delay(
            Timeout.Infinite,
            cancellationToken: stoppingToken
        );
    }
}
