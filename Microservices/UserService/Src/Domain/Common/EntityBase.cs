namespace Domain.Common;

public class EntityBase
{
    public guid Id { get; set; }
    public EntityBase()
    {
        Id = guid.newguid();
    }
}
