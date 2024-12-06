using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;

/// <summary>Represents the base interface for result objects, providing access to reasons, errors, and successes.</summary>
public interface IResultBase
{
    /// <summary>Indicates whether the result contains at least one error.</summary>
    bool IsFailed { get; }

    /// <summary>Indicates whether the result contains no errors.</summary>
    bool IsSuccess { get; }

    /// <summary>Gets all reasons, including errors and successes.</summary>
    List<IReason> Reasons { get; }

    /// <summary>Gets all errors contained in the result.</summary>
    List<IError> Errors { get; }

    /// <summary>Gets all successes contained in the result.</summary>
    List<ISuccess> Successes { get; }
}
