using Domain.ValueObjects;

namespace Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public float Price { get; private set; }
    public int Stock { get; private set; }

    public Product(string name, float price, int stock)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Stock = stock;
    }
    public Product() { }
}
