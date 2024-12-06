namespace SharedKernel.Primitives.Result;

/// <summary>Represents a non-generic result indicating success or failure.</summary>
public partial class Result
{
    /// <summary>Creates a successful <see cref="Result"/>.</summary>
    /// <returns>A successful <see cref="Result"/>.</returns>
    public static Result Ok() => new();

    /// <summary>Creates a successful <see cref="Result{TValue}"/> with the given value.</summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="value">The value to include in the result.</param>
    /// <returns>A successful <see cref="Result{TValue}"/> containing <paramref name="value"/>.</returns>
    public static Result<TValue> Ok<TValue>(TValue value) => new Result<TValue>().WithValue(value);
}
