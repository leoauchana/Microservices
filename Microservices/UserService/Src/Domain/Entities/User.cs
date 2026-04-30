using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class User : EntityBase
{
    public string Username { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public Email Email { get; private set; } = null!;

    public User(string username, string password, Email email)
    {
        Username = username;
        Password = password;
        Email = email;
    }
}



