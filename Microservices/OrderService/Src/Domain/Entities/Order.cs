using Domain.Common;

namespace Domain.Entities;

public class Order : EntityBase
{
    public Guid UserId {  get; private set; }
    public DateOnly Date {  get; private set; }
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    public float Total => _orderItems.Sum(oi => oi.SubTotal);
    public Order(Guid userId)
    {
        UserId = userId;
        Date = DateOnly.FromDateTime(DateTime.Now);
    }
    public Order() { }
    public void AddOrderItem(OrderItem newOrder) => _orderItems.Add(newOrder);
}
