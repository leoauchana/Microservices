public record OrderCreatedEvent(
    Guid idOrder,
    decimal total,
    DateOnly creationDate,
    Dictionary<Guid, int> productStock
);