namespace Application.DTOs;

public class ProductDto
{
    public record Request(string name, string description, float price, int stock);
    public record Response(Guid id, string name, string description, float price, int stock);
}
