namespace Application.DTOs;

public class ProductDto
{
    public record Request(string name, string description, decimal price, int stock);
    public record Response(Guid id, string name, string description, decimal price, int stock);
    public record Stock(string idProduct, int quantity);
    public record ReduceStock(Dictionary<Guid, int> productStock);
}
