namespace Application.DTOs;

public class UserDto
{
    public record Response(string fullName, string email);
    public record GetById(string id, string fullName, string email);
}
