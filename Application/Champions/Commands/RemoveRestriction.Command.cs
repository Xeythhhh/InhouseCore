using Application.Abstractions;

using Domain.Abstractions;
using Domain.Champions;
using SharedKernel.Primitives.Result;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Contracts.v1.Champions.Requests;

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
           await repository.RemoveChampionRestriction(
                    (Champion.ChampionId)command.ChampionId,
                    (Champion.Restriction.RestrictionId)command.RestrictionId, cancellationToken)
                .Tap(() => unitOfWork.SaveChangesAsync(cancellationToken));
    }
    public static Result<RemoveRestrictionCommand> FromRequest(RemoveRestrictionRequest dto) =>
        new RemoveRestrictionCommand(dto.ChampionId, dto.RestrictionId);
}
