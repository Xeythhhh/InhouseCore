namespace Domain.Primitives;

/// <summary>Represents the base class all value objects derive from.</summary>
public abstract record ValueObject
{
    /// <inheritdoc />
    public virtual bool Equals(ValueObject? other) =>
        other is not null &&
        GetAtomicValues().SequenceEqual(other.GetAtomicValues());

    /// <inheritdoc />
    public override int GetHashCode()
    {
        HashCode hashCode = default;

        foreach (object obj in GetAtomicValues())
        {
            hashCode.Add(obj);
        }

        return hashCode.ToHashCode();
    }

    /// <summary>Gets the atomic values of the value object.</summary>
    /// <returns>The collection of objects representing the value object values.</returns>
    protected abstract IEnumerable<object> GetAtomicValues();
}
