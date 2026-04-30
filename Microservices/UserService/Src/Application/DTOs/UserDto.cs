namespace Application.DTOs;

public class UserDto
{
    public record Request(string userName, string email, string password);
    public record Response(string id, string userName, string email);
}
