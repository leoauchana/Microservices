namespace Application.DTOs;

public class ProductDto
{
    // DTOs for response de endpoints
    public record Response(string name, float price);

    // DTOs for response of services 
    public record ValidateProduct(float price, int stock);
    public record Get(string id, string name, float price, int stock);
}
