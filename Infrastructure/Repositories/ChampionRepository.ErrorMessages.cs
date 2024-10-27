using SharedKernel.Primitives.Reasons;

namespace Infrastructure.Repositories;

public partial class ChampionRepository
{
    public sealed class GetAllChampionsError() : Error("An error occurred while retrieving Champions.");
    public sealed class GetChampionError() : Error("An error occurred while retrieving Champion.");
    public sealed class AddChampionError() : Error("An error occurred while adding the Champion.");
    public sealed class UpdateChampionError() : Error("An error occurred while updating the Champion.");
    public sealed class DeleteChampionError() : Error("An error occurred while deleting Champions.");
    public sealed class ChampionNotFoundError() : Error("Champion not found.");

    public sealed class RemoveRestrictionError(string reason = "") : Error($"An error occurred while removing the Champion Restriction. {reason}");
    public sealed class EditRestrictionError(string reason = "") : Error($"An error occurred while editing the Champion Restriction. {reason}");
    public sealed class GetRestrictionError(string reason = "") : Error($"An error occurred while retrieving the Champion Restriction. {reason}");

    public sealed class GetAugmentError(string reason = "") : Error($"An error occurred while retrieving the Champion Augment. {reason}");
    public sealed class RemoveAugmentError(string reason = "") : Error($"An error occurred while removing the Champion Augment. {reason}");
    public sealed class EditAugmentError(string reason = "") : Error($"An error occurred while editing the Champion Augment. {reason}");
}
