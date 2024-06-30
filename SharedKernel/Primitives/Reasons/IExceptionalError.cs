namespace SharedKernel.Primitives.Reasons;
public interface IExceptionalError : IError
{
    Exception Exception { get; }
}