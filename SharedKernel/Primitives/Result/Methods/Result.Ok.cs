namespace SharedKernel.Primitives.Result;
public partial class Result
{
    /// <summary>Creates a success result</summary>
    public static Result Ok() => new();

    /// <summary>Creates a success result with the given value</summary>
    public static Result<TValue> Ok<TValue>(TValue value)
    {
        Result<TValue> result = new();
        result.WithValue(value);
        return result;
    }
}
