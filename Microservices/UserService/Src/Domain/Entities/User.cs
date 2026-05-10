using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class User : EntityBase
{
    public string Username { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public string FullName { get; set; } = null!;
    public Email Email { get; private set; } = null!;

    public User(string username, string password, string fullName, Email email)
    {
        Username = username;
        Password = password;
        FullName = fullName;
        Email = email;
    }
}



