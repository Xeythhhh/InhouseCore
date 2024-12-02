using Application.Abstractions;

using Domain.Abstractions;
using Domain.Champions;

using SharedKernel.Contracts.v1.Champions;
using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;
using SharedKernel.Extensions.ResultExtensions;

namespace Application.Champions.Commands;

public sealed record class UpdateRestrictionCommand(
    long RestrictionId,
    long AugmentId,
    long? AugmentId2,
    string Reason) :
    ICommand
{
    internal sealed class Handler(
        IChampionRepository repository,
        IUnitOfWork unitOfWork) :
        ICommandHandler<UpdateRestrictionCommand>
    {
        public async Task<Result> Handle(UpdateRestrictionCommand command, CancellationToken cancellationToken) =>
            await repository.GetRestrictionById((Champion.Restriction.RestrictionId)command.RestrictionId, cancellationToken)
                .Bind(restriction => UpdateRestriction(command, restriction, cancellationToken))
                .Tap(() => unitOfWork.SaveChangesAsync(cancellationToken));

        private async Task<Result> UpdateRestriction(UpdateRestrictionCommand command, Champion.Restriction restriction, CancellationToken cancellationToken)
        {
            try
            {
                restriction.Augment = (await repository.GetAugmentById((Champion.Augment.AugmentId)command.AugmentId, cancellationToken)).Value;
                restriction.Augment2 = command.AugmentId2 is null ? null
                    : (await repository.GetAugmentById((Champion.Augment.AugmentId)command.AugmentId2, cancellationToken)).Value;
                restriction.Reason = command.Reason;

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error($"Failed to update restriction with id '{command.RestrictionId}'.")
                    .CausedBy(ex));
            }
        }
    }

    public static Result<UpdateRestrictionCommand> FromRequest(UpdateRestrictionRequest dto) =>
        new UpdateRestrictionCommand(
            dto.RestrictionId,
            dto.AugmentId,
            dto.AugmentId2,
            dto.Reason);
}
