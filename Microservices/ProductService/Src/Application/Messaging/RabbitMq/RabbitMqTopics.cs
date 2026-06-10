namespace Application.Messaging.RabbitMq;

public static class RabbitMqTopics
{
    public const string ProductsExchange = "products";
    public const string ProductCreatedRoutingKey = "product.created";
}
