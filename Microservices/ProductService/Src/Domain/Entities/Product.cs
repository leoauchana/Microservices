namespace Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public float Price { get; private set; }
    public int Stock { get; private set; }

    public Product(string name, string description, float price, int stock)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }
    public Product() { }
}
