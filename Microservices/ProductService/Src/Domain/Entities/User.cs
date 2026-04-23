using Domain.ValueObjects;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string UserName { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public Mail Mail { get; private set; } = null!;

    public User(string userName, string password, Mail mail)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Password = password;
        Mail = mail;
    }
    public User() { }
}
