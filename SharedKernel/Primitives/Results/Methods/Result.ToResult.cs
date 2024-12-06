namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Converts the current result to a generic <see cref="Result{TNewValue}"/> with no value.</summary>
    /// <typeparam name="TNewValue">The type of the value for the new result.</typeparam>
    public Result<TNewValue> ToResult<TNewValue>() =>
        new Result<TNewValue>()
            .WithReasons(Reasons);

    /// <summary>Converts the current result to a generic <see cref="Result{TNewValue}"/> with a value.</summary>
    /// <typeparam name="TNewValue">The type of the value for the new result.</typeparam>
    /// <param name="newValue">The value to associate with the new result.</param>
    /// <param name="validator">The validator for the new value.</param>
    public Result<TNewValue> ToResult<TNewValue>(TNewValue newValue, Func<TNewValue, bool>? validator = null) =>
        IsFailed || (validator != null && !validator(newValue))
            ? ToResult<TNewValue>()
            : ToResult<TNewValue>()
                .WithValue(newValue);
}
