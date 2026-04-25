namespace Domain.Entities;

public class User : EntityBase
{
    public string Username { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public string Email { get; private set; } = null!;

    public User(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }
}



