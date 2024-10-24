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
    string AbilityName,
    string AbilityIdentifier,
    string Color,
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
                .Bind(restriction => UpdateRestriction(command, restriction))
                .Tap(() => unitOfWork.SaveChangesAsync(cancellationToken));

        private static Result UpdateRestriction(UpdateRestrictionCommand command, Champion.Restriction restriction)
        {
            try
            {
                restriction.AbilityName = command.AbilityName;
                restriction.Identifier = command.AbilityIdentifier;
                restriction.ColorHex = command.Color;
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
        new UpdateRestrictionCommand(long.Parse(dto.RestrictionId), dto.AbilityName, dto.AbilityIdentifier, dto.Color, dto.Reason);
}
