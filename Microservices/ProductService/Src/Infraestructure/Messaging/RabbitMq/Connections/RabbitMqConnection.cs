using Infraestructure.Messaging.RabbitMq.Configurations;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infraestructure.Messaging.RabbitMq.Connections;
public class RabbitMqConnection : IDisposable
{
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly ConnectionFactory _connectionFactory;
    private IConnection? _connection;
    public RabbitMqConnection(IOptions<RabbitMqOptions> rabbitMqOptions)
    {
        _rabbitMqOptions = rabbitMqOptions.Value;

        _connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqOptions.HostName,
            UserName = _rabbitMqOptions.UserName,
            Password = _rabbitMqOptions.Password,
        };
    }        
    public async Task<IConnection> CreateConnectionAsync()
    {
        if(_connection is null)
            return await _connectionFactory.CreateConnectionAsync();
        return _connection;
    }
    public void Dispose()
    {
        _connection?.Dispose();
    }
}
