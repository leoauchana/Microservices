namespace Application.Exceptions;

public class FormatInvalidException : ApplicationException
{
    public FormatInvalidException(string message) : base(message)
    {
    }
}
