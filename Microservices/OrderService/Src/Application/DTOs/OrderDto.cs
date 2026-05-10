namespace Application.DTOs;

public class OrderDto
{
    public record Create(string idUser, List<OrderItemDto.Create> orderItems);
    public record GetById(string id, UserDto.Response user, List<OrderItemDto.Response> orderItems, float total, DateOnly date);
    public record GetAll(string id, UserDto.Response user, int orderCount, float total, DateOnly date);
}
