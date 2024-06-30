namespace SharedKernel.Primitives.Reasons;
public interface IError : IReason
{
    /// <summary>/// Reasons of the error</summary>
    List<IError> Reasons { get; }
}