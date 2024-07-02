using MediatR;

using SharedKernel.Primitives.Result;

namespace Application.Abstractions;

public interface IBaseCommand;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;
