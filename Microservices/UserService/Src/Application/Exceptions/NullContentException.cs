namespace Application.Exceptions;

public class NullContentException : ApplicationException
{
    public NullContentException(string message) : base(message)
    {
    }
}
