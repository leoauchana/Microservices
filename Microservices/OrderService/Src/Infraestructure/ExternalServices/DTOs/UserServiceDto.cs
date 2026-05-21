namespace Infraestructure.ExternalServices.DTOs;

public class UserServiceDto
{
    public record Response(Get userFound);
    public record Get(string id, string userName, string fullName, string email);
}
