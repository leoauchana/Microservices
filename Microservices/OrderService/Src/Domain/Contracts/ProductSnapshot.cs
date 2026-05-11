namespace Domain.Contracts;

public record ProductSnapshot
(
    string id,
    string name,
    float price,
    int stock
);