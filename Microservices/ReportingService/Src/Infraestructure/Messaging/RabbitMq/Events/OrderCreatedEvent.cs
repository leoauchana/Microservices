namespace Infraestructure.Messaging.RabbitMq.Events;

public record OrderCreatedEvent(
    Guid idOrder,
    decimal total,
    DateTime creationDate,
    Dictionary<Guid, int> productStock
);
