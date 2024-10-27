using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Create a success/failed result depending on the parameter isFailure</summary>
    public static Result FailIf(bool isFailure, string error) =>
        isFailure ? Fail(error) : Ok();

    /// <summary>Create a success/failed result depending on the parameter isFailure</summary>
    /// <remarks>Error is lazily evaluated.</remarks>
    public static Result FailIf(bool isFailure, Func<IError> errorFactory) =>
        isFailure ? Fail(errorFactory.Invoke()) : Ok();

    /// <summary>Create a success/failed result depending on the parameter isFailure</summary>
    /// <remarks>Error is lazily evaluated.</remarks>
    public static Result FailIf(bool isFailure, Func<string> errorMessageFactory) =>
        isFailure ? Fail(errorMessageFactory.Invoke()) : Ok();

    /// <summary>Create a success/failed result depending on the parameter isFailure</summary>
    public static Result FailIf(bool isFailure, IError error) =>
        isFailure ? Fail(error) : Ok();
}
