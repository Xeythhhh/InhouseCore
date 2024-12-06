using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;

/// <summary>Provides a base implementation for result objects, containing reasons, errors, and successes.</summary>
public abstract partial class ResultBase : IResultBase
{
    /// <summary>Initializes a new instance of the <see cref="ResultBase"/> class.</summary>
    protected ResultBase() => Reasons = new();

    /// <inheritdoc/>
    public bool IsFailed => Reasons.OfType<IError>().Any();

    /// <inheritdoc/>
    public bool IsSuccess => !IsFailed;

    /// <inheritdoc/>
    public List<IReason> Reasons { get; }

    /// <inheritdoc/>
    public List<IError> Errors => Reasons.OfType<IError>().ToList();

    /// <inheritdoc/>
    public List<ISuccess> Successes => Reasons.OfType<ISuccess>().ToList();

    /// <summary>Deconstructs the result into success and failure states.</summary>
    /// <param name="isSuccess">Indicates whether the result is successful.</param>
    /// <param name="isFailed">Indicates whether the result has failed.</param>
    public void Deconstruct(out bool isSuccess, out bool isFailed)
    {
        isSuccess = IsSuccess;
        isFailed = IsFailed;
    }

    /// <summary>Deconstructs the result into success, failure states, and a list of errors.</summary>
    /// <param name="isSuccess">Indicates whether the result is successful.</param>
    /// <param name="isFailed">Indicates whether the result has failed.</param>
    /// <param name="errors">A list of errors, if the result has failed.</param>
    public void Deconstruct(out bool isSuccess, out bool isFailed, out List<IError> errors)
    {
        isSuccess = IsSuccess;
        isFailed = IsFailed;
        errors = IsFailed ? Errors : new();
    }

    /// <summary>Converts a collection of error reasons into a single string representation.</summary>
    /// <param name="errorReasons">The collection of error reasons.</param>
    /// <returns>A concatenated string of error messages.</returns>
    internal static string ErrorReasonsToString(IReadOnlyCollection<IError> errorReasons) =>
        string.Join("; ", errorReasons);
}
