using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Extensions.ResultExtensions;
public static class ReasonExtensions
{
    public static bool HasMetadataKey(this IReason reason, string key) =>
        string.IsNullOrEmpty(key) ? throw new ArgumentNullException(nameof(key))
            : reason.Metadata.ContainsKey(key);

    public static bool HasMetadata(this IReason reason, string key, Func<object, bool> predicate) =>
        string.IsNullOrEmpty(key) ? throw new ArgumentNullException(nameof(key))
            : predicate == null ? throw new ArgumentNullException(nameof(predicate))
            : reason.Metadata.TryGetValue(key, out object? actualValue) && predicate(actualValue);
}