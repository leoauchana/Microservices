namespace Application.DTOs;

public class OrderDto
{
	// DTOs specific
	public record GetOrderByDateRequest(int page = 1, int pageSize = 10, DateOnly? date = null);

	public record GetOrderByDateResponse();

	// DTOs generic
	public record Response(string id, 
						   decimal total, 
						   DateOnly creationDate,
						   int orderItemCount);
}
