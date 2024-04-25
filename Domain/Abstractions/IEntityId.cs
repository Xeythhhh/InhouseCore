namespace Domain.Abstractions;

/// <summary>
/// Interface for strongly typed Ids
/// </summary>
public interface IEntityId
{
    public Ulid Value { get; init; }
}
