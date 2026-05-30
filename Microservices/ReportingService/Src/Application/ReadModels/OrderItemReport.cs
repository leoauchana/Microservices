namespace Application.ReadModels;

public class OrderItemReport
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public OrderItemReport(Guid id, Guid orderId, Guid productId, int quantity,
    decimal price)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
}
