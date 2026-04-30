namespace Domain.Common;

public class EntityBase
{
    public Guid Id { get; set; }
	public EntityBase()
	{
		Id = Guid.NewGuid();
	}
}
