#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Domain.Primitives;

/// <summary>Represents the base class all value objects derive from.</summary>
public abstract record ValueObject<T> : IEquatable<T>
{
    public T Value { get; protected set; }

    /// <inheritdoc />
    public virtual bool Equals(ValueObject<T>? other) =>
        other is not null &&
        GetAtomicValues().SequenceEqual(other.GetAtomicValues());

    /// <inheritdoc />
    public bool Equals(T? other) =>
        other is not null &&
        GetAtomicValues().SequenceEqual([other]);

    /// <inheritdoc />
    public override int GetHashCode()
    {
        HashCode hashCode = default;

        foreach (object? obj in GetAtomicValues())
        {
            hashCode.Add(obj);
        }

        return hashCode.ToHashCode();
    }

    /// <summary>Gets the atomic values of the value object.</summary>
    /// <returns>The collection of objects representing the value object values.</returns>
    protected abstract IEnumerable<object?> GetAtomicValues();
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.