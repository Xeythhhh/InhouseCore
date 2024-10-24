using Application.Abstractions;

using Domain.Champions;

using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Commands;

public sealed record class AddRestrictionCommand(
    long ChampionId,
    string Target,
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
                .Bind(champion => champion.AddRestriction(command.Target, command.Reason))
                .Bind(repository.Update)
                .OnSuccessTry(() => unitOfWork.SaveChangesAsync(cancellationToken));
    }

    public static Result<AddRestrictionCommand> FromRequest(AddRestrictionRequest dto) =>
        new AddRestrictionCommand(long.Parse(dto.ChampionId), dto.Target, dto.Reason);
}
