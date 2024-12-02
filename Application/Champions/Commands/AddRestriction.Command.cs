using Application.Abstractions;

using Domain.Abstractions;
using Domain.Champions;

using SharedKernel.Contracts.v1.Champions.Requests;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Commands;

public sealed record class AddRestrictionCommand(
    long ChampionId,
    long AugmentId,
    long? AugmentId2,
    string Reason) :
    ICommand
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<AddRestrictionCommand>
    {
        public async Task<Result> Handle(AddRestrictionCommand command, CancellationToken cancellationToken) =>
            await repository.GetById((Champion.ChampionId)command.ChampionId, cancellationToken)
                .Bind(champion => champion.AddRestriction(
                    command.Reason,
                    champion.Augments.Single(a => a.Id == command.AugmentId),
                    champion.Augments.SingleOrDefault(a => a.Id == command.AugmentId2)))
                .Bind(repository.Update)
                .Tap(() => unitOfWork.SaveChangesAsync(cancellationToken));
    }

    public static Result<AddRestrictionCommand> FromRequest(AddRestrictionRequest dto) =>
        new AddRestrictionCommand(
            dto.ChampionId,
            dto.AugmentId,
            dto.AugmentId2,
            dto.Reason);
}
