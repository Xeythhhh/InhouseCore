namespace Domain.Primitives;

/// <summary>Interface for defining a strongly-typed entity identifier.</summary>
public interface IEntityId
{
    /// <summary>Gets the value of the entity identifier.</summary>
    public long Value { get; init; }
}