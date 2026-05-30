using Infraestructure.Messaging.RabbitMq.Configurations;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infraestructure.Messaging.RabbitMq.Connection;

public class RabbitMqConnection
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly RabbitMqOptions _rabbitMqOptions;
    private IConnection? _connection;
    public RabbitMqConnection(IOptions<RabbitMqOptions> rabbitMqOptions)
    {
        _rabbitMqOptions = rabbitMqOptions.Value;
        _connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqOptions.HostName,
            UserName = _rabbitMqOptions.UserName,
            Password = _rabbitMqOptions.Password
        };
    }
    public async Task<IConnection> CreateConnectionAsync()
    {
        if (_connection is null)
        {
            return await _connectionFactory.CreateConnectionAsync(); ;
        }
        return _connection;
    }
}