namespace Domain.Entities;

/// <summary>
/// Interface for strongly typed Ids
/// </summary>
public interface IEntityId
{
    public Ulid Value { get; init; }
}
