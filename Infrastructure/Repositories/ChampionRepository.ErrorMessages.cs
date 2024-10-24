using SharedKernel.Primitives.Reasons;

namespace Infrastructure.Repositories;

public partial class ChampionRepository
{
    public sealed class GetAllError() : Error("An error occurred while retrieving Champions.");
    public sealed class GetError() : Error("An error occurred while retrieving Champion.");
    public sealed class AddError() : Error("An error occurred while adding the Champion.");
    public sealed class UpdateError() : Error("An error occurred while updating the Champion.");
    public sealed class DeleteError() : Error("An error occurred while deleting Champions.");
    public sealed class DeleteRestrictionError() : Error("An error occurred while deleting Champion Restriction.");
    public sealed class NotFoundError() : Error("Champion not found.");
    public sealed class RemoveRestrictionError(string reason = "") : Error($"An error occurred while removing the Champion Restriction. {reason}");
    public sealed class EditRestrictionError(string reason = "") : Error($"An error occurred while editing the Champion Restriction. {reason}");
}
