namespace Infraestructure.ExternalServices.DTOs;

public class ProductServiceDto
{
    public record List(List<Get> productsFound, int count);
    public record Get(string id, string name, float price, int stock);
}
