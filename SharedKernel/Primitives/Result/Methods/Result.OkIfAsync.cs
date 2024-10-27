using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result> OkIfAsync(Task<bool> isSuccess, IError error) =>
        await isSuccess ? Ok() : Fail(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result> OkIfAsync(Task<bool?> isSuccess, IError error) =>
        await isSuccess ?? false
            ? Ok()
            : Fail(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result> OkIfAsync(Task<bool> isSuccess, string error) =>
        await isSuccess ? Ok() : Fail(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result> OkIfAsync(Task<bool?> isSuccess, string error) =>
        await isSuccess ?? false
            ? Ok()
            : Fail(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    /// <remarks>Error is lazily evaluated.</remarks>
    public static async Task<Result> OkIfAsync(Task<bool> isSuccess, Func<IError> errorFactory) =>
        await isSuccess ? Ok() : Fail(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    /// <remarks>Error is lazily evaluated.</remarks>
    public static async Task<Result> OkIfAsync(Task<bool?> isSuccess, Func<IError> errorFactory) =>
        await isSuccess ?? false
            ? Ok()
            : Fail(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    /// <remarks>Error is lazily evaluated.</remarks>
    public static async Task<Result> OkIfAsync(Task<bool> isSuccess, Func<string> errorFactory) =>
        await isSuccess ? Ok() : Fail(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    /// <remarks>Error is lazily evaluated.</remarks>
    public static async Task<Result> OkIfAsync(Task<bool?> isSuccess, Func<string> errorFactory) =>
        await isSuccess ?? false
            ? Ok()
            : Fail(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result<T>> OkIfAsync<T>(T value, Task<bool> isSuccess, IError error) =>
        await isSuccess ? Ok(value) : Fail<T>(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result<T>> OkIfAsync<T>(T value, Task<bool?> isSuccess, IError error) =>
        await isSuccess ?? false
            ? Ok(value)
            : Fail<T>(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result<T>> OkIfAsync<T>(T value, Task<bool> isSuccess, string error) =>
        await isSuccess ? Ok(value) : Fail<T>(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result<T>> OkIfAsync<T>(T value, Task<bool?> isSuccess, string error) =>
        await isSuccess ?? false
            ? Ok(value)
            : Fail<T>(error);

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result<T>> OkIfAsync<T>(T value, Task<bool> isSuccess, Func<IError> errorFactory) =>
        await isSuccess ? Ok(value) : Fail<T>(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result<T>> OkIfAsync<T>(T value, Task<bool?> isSuccess, Func<IError> errorFactory) =>
        await isSuccess ?? false
            ? Ok(value)
            : Fail<T>(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result<T>> OkIfAsync<T>(T value, Task<bool> isSuccess, Func<string> errorFactory) =>
        await isSuccess ? Ok(value) : Fail<T>(errorFactory.Invoke());

    /// <summary>Create a success/failed result depending on the parameter isSuccess</summary>
    public static async Task<Result<T>> OkIfAsync<T>(T value, Task<bool?> isSuccess, Func<string> errorFactory) =>
        await isSuccess ?? false
            ? Ok(value)
            : Fail<T>(errorFactory.Invoke());
}
