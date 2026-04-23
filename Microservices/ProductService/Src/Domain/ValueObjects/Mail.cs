namespace Domain.ValueObjects;

public class Mail
{
    private string _value { get; }

    private Mail(string value)
    {
        _value = value;
    }

    public static Mail Create(string value)
    {
        // Aquí puedes agregar validaciones para asegurarte de que el correo electrónico sea válido
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email no puede ser vacío.");

        if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Email con formato inválido.");

        return new Mail(value);
    }

}