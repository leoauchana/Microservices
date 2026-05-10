namespace Application.DTOs;

public class OrderItemDto
{
    public record Create(string idProduct, int quantity);
    public record Response(ProductDto.Response product, int quantity, float subtotal);
}
