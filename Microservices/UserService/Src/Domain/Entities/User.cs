namespace Domain.Entities;

public class User : EntityBase
{
    public string Username { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public string Mail { get; private set; } = null!;

    public User(string username, string password, string mail)
    {
        Username = username;
        Password = password;
        Mail = mail;
    }
}



