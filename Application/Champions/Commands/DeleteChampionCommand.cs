using Application.Abstractions;

using Domain.Champions;

using SharedKernel.Contracts.Requests.Champions;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Commands;

public sealed record class DeleteChampionCommand(long Id) : ICommand
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<DeleteChampionCommand>
    {
        public async Task<Result> Handle(DeleteChampionCommand request, CancellationToken cancellationToken) =>
            (await repository.GetById((ChampionId)request.Id, cancellationToken)
                .Bind(repository.Delete)
                .OnSuccessTry(() => unitOfWork.SaveChangesAsync(cancellationToken)))
                .ToResult();
    }
}