using Domain.Common;

namespace Domain.Entities;

public class Product : EntityBase
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public float Price { get; private set; }
    public int Stock { get; private set; }

    public Product(string name, string description, float price, int stock)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }
    public Product() { }

    public void ReduceStock(int quantity)
    {
        Stock -= quantity;
    }
}
