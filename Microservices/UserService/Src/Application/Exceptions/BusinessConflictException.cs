namespace Application.Exceptions;

public class BusinessConflictException : ApplicationException
{
    public BusinessConflictException(string message) : base(message)
    {
    }
}
