namespace BuildingBlocks.Domain;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; protected set; }

    public DateTime UpdatedAt { get; protected set; }
}