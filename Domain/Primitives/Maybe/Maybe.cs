namespace Domain.Primitives.Maybe;

/// <summary>Represents a wrapper around a value that may or may not be null.</summary>
/// <typeparam name="T">The value type.</typeparam>
public sealed class Maybe<T> :
    IEquatable<Maybe<T>>
{
    private readonly T _value;

    /// <summary>Initializes a new instance of the <see cref="Maybe{T}"/> class.</summary>
    /// <param name="value">The value.</param>
    private Maybe(T value) => _value = value;

    /// <summary>Gets a value indicating whether the value exists.</summary>
    public bool HasValue => !HasNoValue;

    /// <summary>Gets a value indicating whether the value does not exist.</summary>
    public bool HasNoValue => _value is null;

    /// <summary>Gets the value.</summary>
    /// <exception cref="InvalidOperationException">Thrown when trying to access the value when it does not exist.</exception>
    public T Value => HasValue
        ? _value
        : throw new InvalidOperationException("The value can not be accessed because it does not exist.");

    /// <summary>Gets the default empty instance.</summary>
    public static Maybe<T> None => new(default!);

    /// <summary>Creates a new <see cref="Maybe{T}"/> instance based on the specified value.</summary>
    /// <param name="value">The value.</param>
    /// <returns>The new <see cref="Maybe{T}"/> instance.</returns>
    public static Maybe<T> From(T value) => new(value);

    /// <summary>Implicitly converts a value to a <see cref="Maybe{T}"/> instance.</summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Maybe<T>(T value) => From(value);

    /// <summary>Implicitly converts a <see cref="Maybe{T}"/> instance to its underlying value.</summary>
    /// <param name="maybe">The <see cref="Maybe{T}"/> instance to convert.</param>
    public static implicit operator T(Maybe<T> maybe) => maybe.Value;

    /// <inheritdoc />
    public bool Equals(Maybe<T>? other) =>
        other is not null && (
            (HasNoValue && other.HasNoValue) ||
            (HasValue && other.HasValue &&
            Value!.Equals(other.Value))
        );

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj switch
        {
            null => false,
            T value => Equals(new Maybe<T>(value)),
            Maybe<T> maybe => Equals(maybe),
            _ => false
        };

    /// <inheritdoc />
    public override int GetHashCode() => HasValue ? Value!.GetHashCode() : 0;
}
