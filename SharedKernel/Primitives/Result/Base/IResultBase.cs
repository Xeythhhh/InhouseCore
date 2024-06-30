using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;

public interface IResultBase
{
    /// <summary>Is true if Reasons contains at least one error</summary>
    bool IsFailed { get; }

    /// <summary>Is true if Reasons contains no errors</summary>
    bool IsSuccess { get; }

    /// <summary>Get all reasons (errors and successes)</summary>
    List<IReason> Reasons { get; }

    /// <summary>Get all errors</summary>
    List<IError> Errors { get; }

    /// <summary>Get all successes</summary>
    List<ISuccess> Successes { get; }
}
