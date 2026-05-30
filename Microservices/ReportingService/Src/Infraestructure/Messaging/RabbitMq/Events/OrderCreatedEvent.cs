namespace Infraestructure.Messaging.RabbitMq.Events;

public record OrderCreatedEvent(
    string idOrder,
    decimal total,
    DateOnly creationDate,
    Dictionary<string, int> productStock
);
