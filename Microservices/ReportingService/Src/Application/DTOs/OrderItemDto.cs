namespace Application.DTOs;

public class OrderItemDto
{
    public record Response(int quantity, decimal price);
}
