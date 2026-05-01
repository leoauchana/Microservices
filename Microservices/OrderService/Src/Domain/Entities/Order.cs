using Domain.Common;

namespace Domain.Entities;

public class Order : EntityBase
{
    public int Number {  get; private set; }
    public Guid UserId {  get; private set; }
    public DateOnly Date {  get; private set; }
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    public Order(int number, Guid userId, DateOnly date)
    {
        Number = number;
        UserId = userId;
        Date = date;
    }
    public Order() { }
    public void AddOrderItem(OrderItem newOrder) => _orderItems.Add(newOrder);
}
