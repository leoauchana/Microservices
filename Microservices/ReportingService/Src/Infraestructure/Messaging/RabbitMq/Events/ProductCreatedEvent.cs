namespace Infraestructure.Messaging.RabbitMq.Events;

public record ProductCreatedEvent(
    Guid id,
    string name,
    string description,
    DateOnly creationDate
);