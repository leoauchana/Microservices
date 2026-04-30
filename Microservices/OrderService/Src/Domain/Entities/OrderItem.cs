using Domain.Common;

namespace Domain.Entities;

public class OrderItem : EntityBase
{
    public Guid ProductId { get; set; }
    public int Quantity {  get; set; }

    public OrderItem(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}
