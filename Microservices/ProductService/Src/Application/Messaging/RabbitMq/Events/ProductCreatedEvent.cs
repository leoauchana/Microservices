namespace Application.Messaging.RabbitMq.Events;

public record ProductCreatedEvent(
    Guid id,
    string name,
    decimal price,
    string description
);
