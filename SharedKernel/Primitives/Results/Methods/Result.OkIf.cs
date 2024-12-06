using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result OkIf(bool isSuccess, IError error) =>
        isSuccess ? Ok() : Fail(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result OkIf(Func<bool> isSuccess, IError error) =>
        isSuccess() ? Ok() : Fail(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result OkIf(bool isSuccess, string error) =>
        isSuccess ? Ok() : Fail(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result OkIf(Func<bool> isSuccess, string error) =>
        isSuccess() ? Ok() : Fail(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    /// <remarks>Error is lazily evaluated.</remarks>
    public static Result OkIf(bool isSuccess, Func<IError> errorFactory) =>
        isSuccess ? Ok() : Fail(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    /// <remarks>Error is lazily evaluated.</remarks>
    public static Result OkIf(Func<bool> isSuccess, Func<IError> errorFactory) =>
        isSuccess() ? Ok() : Fail(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    /// <remarks>Error is lazily evaluated.</remarks>
    public static Result OkIf(bool isSuccess, Func<string> errorMessageFactory) =>
        isSuccess ? Ok() : Fail(errorMessageFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    /// <remarks>Error is lazily evaluated.</remarks>
    public static Result OkIf(Func<bool> isSuccess, Func<string> errorFactory) =>
        isSuccess() ? Ok() : Fail(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result<T> OkIf<T>(T value, bool isSuccess, IError error) =>
        isSuccess ? Ok(value) : Fail<T>(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result<T> OkIf<T>(T value, Func<bool> isSuccess, IError error) =>
        isSuccess() ? Ok(value) : Fail<T>(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result<T> OkIf<T>(T value, bool isSuccess, string error) =>
        isSuccess ? Ok(value) : Fail<T>(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result<T> OkIf<T>(T value, Func<bool> isSuccess, string error) =>
        isSuccess() ? Ok(value) : Fail<T>(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result<T> OkIf<T>(T value, bool isSuccess, Func<IError> errorFactory) =>
        isSuccess ? Ok(value) : Fail<T>(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result<T> OkIf<T>(T value, Func<bool> isSuccess, Func<IError> errorFactory) =>
        isSuccess() ? Ok(value) : Fail<T>(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result<T> OkIf<T>(T value, bool isSuccess, Func<string> errorFactory) =>
        isSuccess ? Ok(value) : Fail<T>(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static Result<T> OkIf<T>(T value, Func<bool> isSuccess, Func<string> errorFactory) =>
        isSuccess() ? Ok(value) : Fail<T>(errorFactory.Invoke());
}
