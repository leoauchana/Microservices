namespace Application.Exceptions;

public class EntityNotFoundEception : ApplicationException
{
    public EntityNotFoundEception(string msg) : base(msg) { }

}
