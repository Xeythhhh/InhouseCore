using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result.Base;

namespace SharedKernel.Primitives.Result;

public partial class Result<TValue> : ResultBase<Result<TValue>>, IResult<TValue>
{
    private TValue _value;

    /// <summary><inheritdoc/></summary>
    public TValue Value
    {
        get
        {
            ThrowIfFailed();
            return _value;
        }
        private set => _value = value;
    }

    /// <summary>Set value</summary>
    public Result<TValue> WithValue(TValue value)
    {
        Value = value;
        return this;
    }

    /// <summary>Map all errors of the result via errorMapper</summary>
    /// <param name="errorMapper"></param>
    /// <returns></returns>
    public Result<TValue> MapErrors(Func<IError, IError> errorMapper) => IsSuccess
            ? this
            : new Result<TValue>()
            .WithErrors(Errors.Select(errorMapper))
            .WithSuccesses(Successes);

    /// <summary>Map all successes of the result via successMapper</summary>
    /// <param name="successMapper"></param>
    /// <returns></returns>
    public Result<TValue> MapSuccesses(Func<ISuccess, ISuccess> successMapper) => new Result<TValue>()
            .WithValue(Value)
            .WithErrors(Errors)
            .WithSuccesses(Successes.Select(successMapper));

    /// <summary>Convert result with value to result without value</summary>
    public Result ToResult() => new Result()
            .WithReasons(Reasons);

    /// <summary>Convert result with value to result with another value. Use valueConverter parameter to specify the value transformation logic</summary>
    public Result<TNewValue> ToResult<TNewValue>(Func<TValue, TNewValue> valueConverter = null!) =>
        Map(valueConverter);

    /// <summary>Convert result with value to result with another value. Use valueConverter parameter to specify the value transformation logic</summary>
    public Result<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> mapLogic) =>
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

    public static implicit operator Result<TValue>(Result result) =>
        result.ToResult<TValue>(default!);

    public static implicit operator Result<object>(Result<TValue> result) =>
        result.ToResult<object>(value => value!);

    public static implicit operator Result<TValue>(TValue value) =>
        value is Result<TValue> result ? result : Result.Ok(value);

    public static implicit operator Result<TValue>(Error error) => Result.Fail(error);

    public static implicit operator Result<TValue>(List<Error> errors) => Result.Fail(errors);

    /// <summary>Deconstruct Result</summary>
    /// <param name="isSuccess"></param>
    /// <param name="isFailed"></param>
    /// <param name="value"></param>
    public void Deconstruct(out bool isSuccess, out bool isFailed, out TValue value)
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
    public void Deconstruct(out bool isSuccess, out bool isFailed, out TValue value, out List<IError> errors)
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