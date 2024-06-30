using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;

public abstract partial class ResultBase : IResultBase
{
    protected ResultBase() => Reasons = new List<IReason>();

    /// <summary><inheritdoc/></summary>
    public bool IsFailed => Reasons.OfType<IError>().Any();
    /// <summary><inheritdoc/></summary>
    public bool IsSuccess => !IsFailed;
    /// <summary><inheritdoc/></summary>
    public List<IReason> Reasons { get; }
    /// <summary><inheritdoc/></summary>
    public List<IError> Errors => Reasons.OfType<IError>().ToList();
    /// <summary><inheritdoc/></summary>
    public List<ISuccess> Successes => Reasons.OfType<ISuccess>().ToList();

    /// <summary>Deconstruct Result </summary>
    /// <param name="isSuccess"></param>
    /// <param name="isFailed"></param>
    public void Deconstruct(out bool isSuccess, out bool isFailed)
    {
        isSuccess = IsSuccess;
        isFailed = IsFailed;
    }

    /// <summary>Deconstruct Result</summary>
    /// <param name="isSuccess"></param>
    /// <param name="isFailed"></param>
    /// <param name="errors"></param>
    public void Deconstruct(out bool isSuccess, out bool isFailed, out List<IError> errors)
    {
        isSuccess = IsSuccess;
        isFailed = IsFailed;
        errors = IsFailed ? Errors : default!;
    }

    internal static string ErrorReasonsToString(IReadOnlyCollection<IError> errorReasons) =>
        string.Join("; ", errorReasons);
}
