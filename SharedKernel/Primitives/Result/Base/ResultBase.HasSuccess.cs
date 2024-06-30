using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;
public abstract partial class ResultBase
{
    /// <summary>Check if the result object contains a success from a specific type</summary>
    public bool HasSuccess<TSuccess>()
        where TSuccess : ISuccess =>
        HasSuccess<TSuccess>(_ => true, out _);

    /// <summary>Check if the result object contains a success from a specific type</summary>
    public bool HasSuccess<TSuccess>(out IEnumerable<TSuccess> result)
        where TSuccess : ISuccess =>
        HasSuccess(_ => true, out result);

    /// <summary>Check if the result object contains a success from a specific type and with a specific condition</summary>
    public bool HasSuccess<TSuccess>(Func<TSuccess, bool> predicate)
        where TSuccess : ISuccess =>
        HasSuccess(predicate, out _);

    /// <summary>Check if the result object contains a success from a specific type and with a specific condition</summary>
    public bool HasSuccess<TSuccess>(Func<TSuccess, bool> predicate, out IEnumerable<TSuccess> result)
        where TSuccess : ISuccess =>
        HasSuccess(Successes, predicate, out result);

    /// <summary>Check if the result object contains a success with a specific condition</summary>
    public bool HasSuccess(Func<ISuccess, bool> predicate, out IEnumerable<ISuccess> result) =>
        HasSuccess(Successes, predicate, out result);

    /// <summary>Check if the result object contains a success with a specific condition</summary>
    public bool HasSuccess(Func<ISuccess, bool> predicate) =>
        HasSuccess(Successes, predicate, out _);

    private static bool HasSuccess<TSuccess>(
        List<ISuccess> successes,
        Func<TSuccess, bool> predicate,
        out IEnumerable<TSuccess> result) where TSuccess : ISuccess
    {
        List<TSuccess> foundSuccesses = successes.OfType<TSuccess>()
            .Where(predicate)
            .ToList();

        if (foundSuccesses.Count != 0)
        {
            result = foundSuccesses;
            return true;
        }

        result = Array.Empty<TSuccess>();
        return false;
    }
}
