using FluentAssertions.Execution;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;
public static partial class ResultExtensions
{
    public static async Task<Result<TNew>> Bind<TOld, TNew>(this Task<Result<TOld>> resultTask, Func<TOld, Task<Result<TNew>>> bind) =>
        await (await resultTask).Bind(bind);

    public static async Task<Result<TNew>> Bind<TOld, TNew>(this Task<Result<TOld>> resultTask, Func<TOld, Result<TNew>> bind) =>
        (await resultTask).Bind(bind);

    public static async Task<Result> Bind<T>(this Task<Result<T>> resultTask, Func<T, Task<Result>> bind) =>
        await (await resultTask).Bind(bind);

    public static async Task<Result> Bind<T>(this Task<Result<T>> resultTask, Func<T, Result> bind) =>
        (await resultTask).Bind(bind);

    public static async Task<Result<TNew>> Bind<TNew>(this Task<Result> resultTask, Func<Task<Result<TNew>>> bind) =>
        await (await resultTask).Bind(bind);

    public static async Task<Result> Bind(this Task<Result> resultTask, Func<Task<Result>> bind) =>
        await (await resultTask).Bind(bind);

    public static async Task<Result> MapErrors(this Task<Result> resultTask, Func<IError, IError> errorMapper) =>
        (await resultTask).MapErrors(errorMapper);

    public static async Task<Result<T>> MapErrors<T>(this Task<Result<T>> resultTask, Func<IError, IError> errorMapper) =>
        (await resultTask).MapErrors(errorMapper);

    public static async Task<Result> MapSuccesses(this Task<Result> resultTask, Func<ISuccess, ISuccess> errorMapper) =>
        (await resultTask).MapSuccesses(errorMapper);

    public static async Task<Result<T>> MapSuccesses<T>(this Task<Result<T>> resultTask, Func<ISuccess, ISuccess> errorMapper) =>
        (await resultTask).MapSuccesses(errorMapper);

    public static async Task<Result<T>> ToResult<T>(this Task<Result> resultTask, T value) =>
        (await resultTask).ToResult(value);
}