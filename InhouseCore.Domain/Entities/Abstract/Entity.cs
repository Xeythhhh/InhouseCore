using System.ComponentModel.DataAnnotations;

namespace InhouseCore.Domain.Entities.Abstract;

/// <summary>
/// Base model for entities.
/// </summary>
public abstract class Entity : IEntity
{
    protected Entity()
    {
        Id = Ulid.NewUlid();
    }

    [Key]
    [ScaffoldColumn(false)]
    public Ulid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; }
}
