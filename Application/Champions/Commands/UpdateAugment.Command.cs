using Application.Abstractions;

using Domain.Abstractions;
using Domain.Champions;

using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;
using SharedKernel.Extensions.ResultExtensions;

namespace Application.Champions.Commands;

public sealed record class UpdateAugmentCommand(
    long AugmentId,
    string AugmentName,
    string AugmentTarget,
    string AugmentColor) :
    ICommand
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<UpdateAugmentCommand>
    {
        public async Task<Result> Handle(UpdateAugmentCommand command, CancellationToken cancellationToken) =>
            await repository.GetAugmentById((Champion.Augment.AugmentId)command.AugmentId, cancellationToken)
                .Bind(augment => UpdateAugment(command, augment))
                .Tap(() => unitOfWork.SaveChangesAsync(cancellationToken));

        private static Result UpdateAugment(UpdateAugmentCommand command, Champion.Augment augment)
        {
            try
            {
                augment.Name = command.AugmentName;
                augment.Target = command.AugmentTarget;
                augment.ColorHex = command.AugmentColor;

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error($"Failed to update augment with id '{command.AugmentId}'.")
                    .CausedBy(ex));
            }
        }
    }

    public static Result<UpdateAugmentCommand> FromRequest(UpdateAugmentRequest dto) =>
        new UpdateAugmentCommand(
            long.Parse(dto.AugmentId),
            dto.AugmentName,
            dto.AugmentTarget,
            dto.AugmentColor);
}
