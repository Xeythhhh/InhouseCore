namespace InhouseCore.Domain.Entities.Abstract;

/// <summary>
/// Base interface for entities.
/// </summary>
internal interface IEntity
{
    public Ulid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}