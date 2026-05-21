public interface IMessagePublisher
{
    Task PublishMessage<T>(string exchange, string routingKey, T message);
}