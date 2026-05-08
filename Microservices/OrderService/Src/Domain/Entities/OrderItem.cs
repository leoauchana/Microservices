using Domain.Common;

namespace Domain.Entities;

public class OrderItem : EntityBase
{
    public Guid ProductId { get; private set; }
    public int Quantity {  get; private set; }
    public Order Order { get; private set; } = null!;
    public float UnitPrice { get; private set; }
    public float SubTotal => UnitPrice * Quantity;
    public OrderItem(Guid productId, int quantity, float unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
    public OrderItem() { }
}
