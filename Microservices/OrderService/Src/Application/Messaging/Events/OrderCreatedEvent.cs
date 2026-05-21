public record OrderCreatedEvent(
    Dictionary<Guid, int> productStock
);