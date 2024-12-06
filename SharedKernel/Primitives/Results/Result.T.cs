using System.Text.Json.Serialization;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result.Base;

namespace SharedKernel.Primitives.Result;

public partial class Result<T> : ResultBase<Result<T>>, IResult<T>
{
    private T _value;

    /// <summary><inheritdoc/></summary>
    public T Value
    {
        get
        {
            ThrowIfFailed();
            return _value;
        }
        private set => _value = value;
    }

    public T ValueOrDefault => IsSuccess ? Value : default!;

    /// <summary>Set value</summary>
    public Result<T> WithValue(T value)
    {
        Value = value;
        return this;
    }

    /// <summary>Map all errors of the result via errorMapper</summary>
    /// <param name="errorMapper"></param>
    /// <returns></returns>
    public Result<T> MapErrors(Func<IError, IError> errorMapper) => IsSuccess
            ? this
            : new Result<T>()
            .WithErrors(Errors.Select(errorMapper))
            .WithSuccesses(Successes);

    /// <summary>Map all successes of the result via successMapper</summary>
    /// <param name="successMapper"></param>
    /// <returns></returns>
    public Result<T> MapSuccesses(Func<ISuccess, ISuccess> successMapper) => new Result<T>()
            .WithValue(Value)
            .WithErrors(Errors)
            .WithSuccesses(Successes.Select(successMapper));

    /// <summary>Convert result with value to result without value</summary>
    public Result ToResult() => new Result()
            .WithReasons(Reasons);

    /// <summary>Convert result with value to result with another value. Use valueConverter parameter to specify the value transformation logic</summary>
    public Result<TNewValue> ToResult<TNewValue>(Func<T, TNewValue> valueConverter = null!) =>
        Map(valueConverter);

    /// <summary>Convert result with value to result with another value. Use valueConverter parameter to specify the value transformation logic</summary>
    public Result<TNewValue> Map<TNewValue>(Func<T, TNewValue> mapLogic) =>
        IsSuccess && mapLogic == null
            ? throw new ArgumentException("If result is success then valueConverter should not be null")
            : new Result<TNewValue>()
                .WithValue(IsFailed ? default! : mapLogic(Value))
                .WithReasons(Reasons);

    public override string ToString()
    {
        string baseString = base.ToString() ?? string.Empty;
        string valueString = Value is not null ? $"Value:{Value}" : string.Empty;
        return $"{baseString}, {valueString}";
    }

    public static implicit operator Result(Result<T> result) =>
        result.IsSuccess ? Result.Ok() : Result.Fail(result.Errors);

    public static implicit operator Result<T>(Result result) =>
        result.ToResult<T>(default!);

    public static implicit operator Result<object>(Result<T> result) =>
        result.ToResult<object>(value => value!);

    public static implicit operator Result<T>(T value) =>
        value is Result<T> result ? result : Result.Ok(value);

    public static implicit operator Result<T>(Error error) => Result.Fail(error);

    public static implicit operator Result<T>(List<Error> errors) => Result.Fail(errors);

    /// <summary>Deconstruct Result</summary>
    /// <param name="isSuccess"></param>
    /// <param name="isFailed"></param>
    /// <param name="value"></param>
    public void Deconstruct(out bool isSuccess, out bool isFailed, out T value)
    {
        isSuccess = IsSuccess;
        isFailed = IsFailed;
        value = IsSuccess ? Value : default!;
    }

    /// <summary>Deconstruct Result</summary>
    /// <param name="isSuccess"></param>
    /// <param name="isFailed"></param>
    /// <param name="value"></param>
    /// <param name="errors"></param>
    public void Deconstruct(out bool isSuccess, out bool isFailed, out T value, out List<IError> errors)
    {
        isSuccess = IsSuccess;
        isFailed = IsFailed;
        value = IsSuccess ? Value : default!;
        errors = IsFailed ? Errors : default!;
    }

    private void ThrowIfFailed()
    {
        if (IsFailed)
            throw new InvalidOperationException($"Result is in status failed. Value is not set. Having: {ErrorReasonsToString(Errors)}");
    }
}