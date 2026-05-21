namespace Infraestructure.Messaging.RabbitMq.Events;

public record OrderCreatedEvent(
    Dictionary<Guid, int> productStock
);
