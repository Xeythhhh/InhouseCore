using Application.Abstractions;
using Domain.Abstractions;
using Domain.Champions;

using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Commands;

public sealed record class RemoveRestrictionCommand(
    long ChampionId,
    long RestrictionId) :
    ICommand
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<RemoveRestrictionCommand>
    {
        public async Task<Result> Handle(RemoveRestrictionCommand command, CancellationToken cancellationToken) =>
            await (await repository.RemoveChampionRestriction(
                    (Champion.ChampionId)command.ChampionId,
                    (Champion.Restriction.RestrictionId)command.RestrictionId, cancellationToken))
                .OnSuccessTry(() => unitOfWork.SaveChangesAsync(cancellationToken));
    }
    public static Result<RemoveRestrictionCommand> FromRequest(RemoveRestrictionRequest dto) =>
        new RemoveRestrictionCommand(long.Parse(dto.ChampionId), long.Parse(dto.RestrictionId));
}