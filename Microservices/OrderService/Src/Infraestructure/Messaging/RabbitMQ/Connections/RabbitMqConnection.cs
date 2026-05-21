using Microsoft.Extensions.Options;
using RabbitMQ.Client;

public class RabbitMqConnection : IDisposable
{
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly ConnectionFactory _connectionFactory;
    private IConnection? _connection;
    public RabbitMqConnection(IOptions<RabbitMqOptions> rabbitMqOptions)
    {
        _rabbitMqOptions = rabbitMqOptions.Value;
        
        _connectionFactory = new ConnectionFactory{
            HostName =  _rabbitMqOptions.HostName,
            UserName = _rabbitMqOptions.UserName,
            Password = _rabbitMqOptions.Password
        };
    }
    public async Task<IConnection> CreateConnection()
    {
        if(_connection is null)
        {
            _connection = await _connectionFactory.CreateConnectionAsync();
            return _connection;
        }
        return _connection;
    }

    public void Dispose()
    {
        _connection!.Dispose();
    }
}