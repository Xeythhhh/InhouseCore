using SharedKernel.Primitives.Reasons;

namespace SharedKernel.Primitives.Result.Base;

public abstract partial class ResultBase
{
    /// <summary>Checks if the result contains a success of type <typeparamref name="TSuccess"/>.</summary>
    public bool HasSuccess<TSuccess>() where TSuccess : ISuccess =>
        HasSuccess<TSuccess>(_ => true, out _);

    /// <summary>Checks if the result contains a success of type <typeparamref name="TSuccess"/>
    /// and outputs the matching successes.</summary>
    public bool HasSuccess<TSuccess>(out IEnumerable<TSuccess> result) where TSuccess : ISuccess =>
        HasSuccess(_ => true, out result);

    /// <summary>Checks if the result contains a success of type <typeparamref name="TSuccess"/>
    /// that matches the specified condition.</summary>
    /// <param name="predicate">The condition to evaluate for the success.</param>
    public bool HasSuccess<TSuccess>(Func<TSuccess, bool> predicate) where TSuccess : ISuccess =>
        HasSuccess(predicate, out _);

    /// <summary>Checks if the result contains a success of type <typeparamref name="TSuccess"/>
    /// that matches the specified condition and outputs the matching successes.</summary>
    /// <param name="predicate">The condition to evaluate for the success.</param>
    public bool HasSuccess<TSuccess>(Func<TSuccess, bool> predicate, out IEnumerable<TSuccess> result) where TSuccess : ISuccess =>
        CheckForSuccess(Successes, predicate, out result);

    /// <summary>Checks if the result contains a success that matches the specified condition.</summary>
    public bool HasSuccess(Func<ISuccess, bool> predicate) =>
        HasSuccess(predicate, out _);

    /// <summary>Checks if the result contains a success that matches the specified condition
    /// and outputs the matching successes.</summary>
    public bool HasSuccess(Func<ISuccess, bool> predicate, out IEnumerable<ISuccess> result) =>
        CheckForSuccess(Successes, predicate, out result);

    /// <summary>Retrieves all successes of type <typeparamref name="TSuccess"/>.</summary>
    public IEnumerable<TSuccess> GetSuccesses<TSuccess>() where TSuccess : ISuccess =>
        Successes.OfType<TSuccess>();

    /// <summary>Retrieves successes with specific metadata values.</summary>
    public IEnumerable<ISuccess> GetSuccessesWithMetadata(string metadataKey, object metadataValue) =>
        Successes.Where(s => s.Metadata.ContainsKey(metadataKey) && s.Metadata[metadataKey].Equals(metadataValue));

    /// <summary>Recursively checks if a collection of <see cref="ISuccess"/> contains a success
    /// that matches the specified condition.</summary>
    private static bool CheckForSuccess<TSuccess>(
        List<ISuccess> successes,
        Func<TSuccess, bool> predicate,
        out IEnumerable<TSuccess> result) where TSuccess : ISuccess
    {
        List<TSuccess> foundSuccesses = successes.OfType<TSuccess>().Where(predicate).ToList();

        if (foundSuccesses.Count > 0)
        {
            result = foundSuccesses;
            return true;
        }

        result = Array.Empty<TSuccess>();
        return false;
    }
}
