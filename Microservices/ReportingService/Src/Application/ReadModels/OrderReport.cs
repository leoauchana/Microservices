namespace Application.ReadModels;

public class OrderReport
{
    public Guid Id { get; private set; }
    public decimal Total { get; private set; }
    public DateOnly CreationDate { get; private set; }
    private readonly List<OrderItemReport> _orderItemReports = new();
    public IReadOnlyCollection<OrderItemReport> OrderItemReports => _orderItemReports;
    public int Count { get; private set; }
    public OrderReport(Guid id, decimal total, DateOnly creationDate, int countTotal)
    {
        Id = id;
        Total = total;
        CreationDate = creationDate;
        Count = countTotal;
    }
    public void AddOrderItemReport(OrderItemReport newOrderItemReport) 
        => _orderItemReports.Add(newOrderItemReport);
}
