namespace Application.ReadModels;

public class ProductReport
{
    public Guid Id{ get; private set; }
    public float Quantity { get; private set; } 

    public ProductReport(Guid id, int quantity)
    {
        Id = id;
        Quantity = quantity;
    }
}
