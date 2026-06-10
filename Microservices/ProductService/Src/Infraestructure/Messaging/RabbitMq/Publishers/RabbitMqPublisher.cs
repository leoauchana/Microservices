using System.Text;
using System.Text.Json;
using Application.Messaging.RabbitMq.Interfaces;
using Infraestructure.Messaging.RabbitMq.Connections;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Infraestructure.Messaging.RabbitMq.Publishers;

public class RabbitMqPublisher : IMessagePublisher
{
    private readonly RabbitMqConnection _rabbitMqConnection;
    private readonly ILogger<RabbitMqPublisher> _logger;
    public RabbitMqPublisher(
        RabbitMqConnection rabbitMqConnection, 
        ILogger<RabbitMqPublisher> logger)
    {
        _rabbitMqConnection = rabbitMqConnection;
        _logger = logger;
    }
    public async Task PublishMessage<T>(string exchange, string routingKey, T message)
    {
        var connection = await _rabbitMqConnection.CreateConnectionAsync();

        using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(
            exchange: exchange,
            type: ExchangeType.Topic,
            durable: true
        );

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        var basicProperties = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json"
        };

        await channel.BasicPublishAsync(
            exchange: exchange,
            routingKey: routingKey,
            basicProperties: basicProperties,
            body: body,
            mandatory: false
        );

        _logger.LogInformation($"The product service publishes the message {nameof(message)}");
    }
}
