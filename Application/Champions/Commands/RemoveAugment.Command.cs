using Application.Abstractions;

using Domain.Abstractions;
using Domain.Champions;
using SharedKernel.Primitives.Result;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Contracts.v1.Champions.Requests;

namespace Application.Champions.Commands;

public sealed record class RemoveAugmentCommand(
    long ChampionId,
    long AugmentId) :
    ICommand
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<RemoveAugmentCommand>
    {
        public async Task<Result> Handle(RemoveAugmentCommand command, CancellationToken cancellationToken) =>
           await repository.RemoveChampionAugment(
                    (Champion.ChampionId)command.ChampionId,
                    (Champion.Augment.AugmentId)command.AugmentId, cancellationToken)
                .Tap(() => unitOfWork.SaveChangesAsync(cancellationToken));
    }
    public static Result<RemoveAugmentCommand> FromRequest(RemoveAugmentRequest dto) =>
        new RemoveAugmentCommand(long.Parse(dto.ChampionId), long.Parse(dto.AugmentId));
}