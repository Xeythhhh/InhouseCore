using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Executes the action. If an exception is thrown within the action then this exception is transformed via the catchHandler to an Error object</summary>
    public static Result Try(Action action, Func<Exception, IError> exceptionHandler = null!)
    {
        exceptionHandler ??= Error.DefaultExceptionalErrorFactory;

        try
        {
            action();
            return Ok();
        }
        catch (Exception e)
        {
            return Fail(exceptionHandler(e));
        }
    }

    /// <summary>Attempts to execute the supplied action. Returns a Result indicating whether the action executed successfully.</summary>
    public static async Task<Result> Try(Func<Task> action) =>
        await Try(action, Error.DefaultExceptionalErrorFactory);

    /// <summary>Executes the action. If an exception is thrown within the action then this exception is transformed via the catchHandler to an Error object</summary>
    public static async Task<Result> Try(Func<Task> action, Func<Exception, IError> exceptionHandler = null!)
    {
        exceptionHandler ??= Error.DefaultExceptionalErrorFactory;

        try
        {
            await action();
            return Ok();
        }
        catch (Exception e)
        {
            return Fail(exceptionHandler(e));
        }
    }

    /// <summary>Attempts to execute the supplied action. Returns a Result indicating whether the action executed successfully.</summary>
    public static Result<TValue> Try<TValue>(Func<TValue> action) =>
        Try(action, Error.DefaultExceptionalErrorFactory);

    /// <summary>Executes the action. If an exception is thrown within the action then this exception is transformed via the catchHandler to an Error object</summary>
    public static Result<TValue> Try<TValue>(Func<TValue> action, Func<Exception, IError> exceptionHandler = null!)
    {
        exceptionHandler ??= Error.DefaultExceptionalErrorFactory;

        try
        {
            return Ok(action());
        }
        catch (Exception e)
        {
            return Fail(exceptionHandler(e));
        }
    }

    /// <summary>Attempts to execute the supplied action. Returns a Result indicating whether the action executed successfully.</summary>
    /// <para>If the function executed successfully, the result contains its return value.</para></summary>
    public static async Task<Result<TValue>> Try<TValue>(Func<Task<TValue>> action) =>
        await Try(action, Error.DefaultExceptionalErrorFactory);

    /// <summary>Executes the action. If an exception is thrown within the action then this exception is transformed via the catchHandler to an Error object</summary>
    public static async Task<Result<TValue>> Try<TValue>(Func<Task<TValue>> action, Func<Exception, IError> exceptionHandler = null!)
    {
        exceptionHandler ??= Error.DefaultExceptionalErrorFactory;

        try
        {
            return Ok(await action());
        }
        catch (Exception e)
        {
            return Fail(exceptionHandler(e));
        }
    }

    /// <summary>Attempts to execute the supplied action. Returns a Result indicating whether the action executed successfully.</summary>
    public static async Task<Result> Try(Task task) =>
        await Try(task, Error.DefaultExceptionalErrorFactory);

    /// <summary>Attempts to execute the supplied action. Returns a Result indicating whether the action executed successfully.</summary>
    public static async Task<Result> Try(Task Task, Func<Exception, IError> exceptionHandler)
    {
        try
        {
            await Task;
            return Ok();
        }
        catch (Exception exception)
        {
            IError error = exceptionHandler(exception);
            return Fail(error);
        }
    }

    /// <summary>Attempts to execute the supplied function. Returns a Result indicating whether the function executed successfully.
    /// <para>If the function executed successfully, the result contains its return value.</para></summary>
    public static async Task<Result<TValue>> Try<TValue>(Task<TValue> task) =>
        await Try(task, Error.DefaultExceptionalErrorFactory);

    /// <summary>Attempts to execute the supplied function. Returns a Result indicating whether the function executed successfully.
    /// <para>If the function executed successfully, the result contains its return value.</para></summary>
    public static async Task<Result<TValue>> Try<TValue>(Task<TValue> task, Func<Exception, IError> exceptionHandler)
    {
        try
        {
            TValue? result = await task;
            return Ok(result);
        }
        catch (Exception exception)
        {
            IError error = exceptionHandler(exception);
            return Fail(error);
        }
    }
}
