using Infraestructure.Messaging.RabbitMq.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infraestructure.Messaging.RabbitMq.Connection;

public class RabbitMqConnection
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly RabbitMqOptions _rabbitMqOptions;
    private IConnection? _connection;
    private readonly ILogger<RabbitMqConnection> _logger;
    public RabbitMqConnection(
        IOptions<RabbitMqOptions> rabbitMqOptions,
        ILogger<RabbitMqConnection> logger)
    {
        _rabbitMqOptions = rabbitMqOptions.Value;
        _logger = logger;
        _connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqOptions.HostName,
            UserName = _rabbitMqOptions.UserName,
            Password = _rabbitMqOptions.Password
        };
    }
    public async Task<IConnection> CreateConnectionAsync()
    {
        var retries = 10;

        while (retries > 0)
        {
            try
            {
                if (_connection is null)
                {
                    return await _connectionFactory.CreateConnectionAsync(); ;
                }
                return _connection;
            }
            catch (Exception ex)
            {
                retries--;

                _logger.LogWarning(ex,
                    $"RabbitMQ unavailable. Retries left: {retries}");

                if (retries == 0)
                    throw;

                await Task.Delay(5000);
            }
        }

        throw new InvalidOperationException();
        // if (_connection is null)
        // {
        //     return await _connectionFactory.CreateConnectionAsync(); ;
        // }
        // return _connection;
    }
}