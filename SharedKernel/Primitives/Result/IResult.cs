using SharedKernel.Primitives.Result.Base;

namespace SharedKernel.Primitives.Result;

public interface IResult<out TValue> : IResultBase
{
    /// <summary>Get the Value. If result is failed then an Exception is thrown because a failed result has no value. Opposite see property ValueOrDefault.</summary>
    TValue Value { get; }
}
