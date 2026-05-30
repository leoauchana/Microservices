namespace Infraestructure.Messaging.RabbitMq;

public static class RabbitMqTopics
{
    public const string OrderExchange = "orders";
    public const string OrderRoutingKey = "order.created";
}
