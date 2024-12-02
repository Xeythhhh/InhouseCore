using Application.Abstractions;

using Domain.Abstractions;
using Domain.Champions;

using SharedKernel.Contracts.v1.Champions.Requests;
using SharedKernel.Extensions.ResultExtensions;
using SharedKernel.Primitives.Result;

namespace Application.Champions.Commands;

public sealed record class AddAugmentCommand(
    long ChampionId,
    string AugmentName,
    string AugmentTarget,
    string AugmentColor) :
    ICommand
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<AddAugmentCommand>
    {
        public async Task<Result> Handle(AddAugmentCommand command, CancellationToken cancellationToken) =>
            await repository.GetById((Champion.ChampionId)command.ChampionId, cancellationToken)
                .Bind(champion => champion.AddAugment(
                    command.AugmentName,
                    command.AugmentTarget,
                    command.AugmentColor,
                    command AugmentIcon))
                .Bind(repository.Update)
                .Tap(() => unitOfWork.SaveChangesAsync(cancellationToken));
    }

    public static Result<AddAugmentCommand> FromRequest(AddAugmentRequest dto) =>
        new AddAugmentCommand(
            dto.ChampionId,
            dto.AugmentName,
            dto.AugmentTarget,
            dto.AugmentColor);
}
