namespace Infraestructure.ExternalServices.DTOs;

public class ProductDto
{
    public record Get(string id, string name, float price, int stock);
}
