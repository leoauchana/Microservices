namespace Application.DTOs;

public class UserDto
{
    public record Create(string userName, string email, string fullName, string password);
    public record Get(string id, string userName, string email, string fullName);
}
