namespace SharedKernel.Primitives.Reasons;
public interface IReason
{
    string Message { get; }
    Dictionary<string, object> Metadata { get; }
}