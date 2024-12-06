using SharedKernel.Primitives.Result.Base;

namespace SharedKernel.Primitives.Result;

/// <summary>Represents a result that may contain a value. A failed result does not have a value.</summary>
/// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
public interface IResult<out TValue> : IResultBase
{
    /// <summary>Gets the value of the result. Throws an exception if the result is failed.
    /// Use <see cref="ValueOrDefault"/> to avoid exceptions for failed results.</summary>
    TValue Value { get; }

    /// <summary>Gets the value of the result, or the default value for <typeparamref name="TValue"/> if the result is failed.</summary>
    TValue ValueOrDefault { get; }
}
