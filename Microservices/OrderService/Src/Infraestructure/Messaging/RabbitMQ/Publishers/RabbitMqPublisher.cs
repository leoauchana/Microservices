using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

public class RabbitMqPublisher : IMessagePublisher
{
    private readonly RabbitMqConnection _rabbitMqConnection;
    public RabbitMqPublisher(RabbitMqConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }
    public async Task PublishMessage<T>(string exchange, string routingKey, T message)
    {
        var connection = await _rabbitMqConnection.CreateConnection();
        using var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(exchange, ExchangeType.Topic, durable: true);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        var basicProperties = new BasicProperties
        {
          Persistent = true,
          ContentType = "application/json"  
        };

        await channel.BasicPublishAsync(
            exchange,
            routingKey,
            mandatory: false,
            basicProperties,
            body
        );
    }
}