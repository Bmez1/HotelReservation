namespace Common;

public abstract class EntityBase<TId>
{
    public TId Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
}
