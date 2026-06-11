namespace Application.DTOs;

public class OrderDto
{
	public record GetOrderByDateRequest(int page = 1, int pageSize = 10, DateOnly? date = null);

	public record GetOrderByDateResponse();
}
