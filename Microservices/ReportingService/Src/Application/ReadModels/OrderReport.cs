namespace Application.ReadModels;

public class OrderReport
{
    public Guid Id { get; private set; }
    public decimal Total { get; private set; }
    public DateOnly CreationDate { get; private set; }
    private readonly List<OrderItemReport> _orderItemReports = new();
    public IReadOnlyCollection<OrderItemReport> OrderItemReports => _orderItemReports;
    public OrderReport(Guid id, decimal total, DateOnly creationDate)
    {
        Id = id;
        Total = total;
        CreationDate = creationDate;
    }
    public void AddOrderItemReport(OrderItemReport newOrderItemReport) 
        => _orderItemReports.Add(newOrderItemReport);
}
