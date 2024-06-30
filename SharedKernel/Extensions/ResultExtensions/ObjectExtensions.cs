using SharedKernel.Primitives.Result;

namespace SharedKernel.Extensions.ResultExtensions;
public static class ObjectExtensions
{
    public static Result<TValue> ToResult<TValue>(this TValue value) =>
        new Result<TValue>()
            .WithValue(value);

    internal static string ToLabelValueStringOrEmpty(this object value, string label) =>
        value.ToString()?.Length != 0
            ? $"{label}='{value}'"
            : string.Empty;
}