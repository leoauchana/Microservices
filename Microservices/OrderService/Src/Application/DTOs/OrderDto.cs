namespace Application.DTOs;

public class OrderDto
{
    public record Request(string idUser, List<OrderItemDto.Request> orderItems);
    public record Response(string id, UserDto.Response user, List<OrderItemDto.Response> orderItems, float total, DateOnly date);
}
