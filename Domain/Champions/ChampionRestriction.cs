using Domain.Champions.ValueObjects;
using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Domain.Champions;

public sealed class ChampionRestriction : EntityBase<ChampionRestriction.RestrictionId>
{
    /// <summary>Strongly-typed Id for <see cref="ChampionRestriction"/>.</summary>
    public sealed record RestrictionId(long Value) :
        EntityId<ChampionRestriction>(Value);

    public RestrictionTarget Target { get; set; }
    public RestrictionReason Reason { get; set; }

    private ChampionRestriction() { }

    public static Result<ChampionRestriction> Create(string target, string reason)
    {
        try
        {
            return CreateInternal((RestrictionTarget)target, (RestrictionReason)reason);
        }
        catch (Exception exception)
        {
            return Result.Fail(new CreateChampionRestrictionError().CausedBy(exception));
        }
    }

    private static Result<ChampionRestriction> CreateInternal(RestrictionTarget target, RestrictionReason reason)
    {
        ChampionRestriction instance = new()
        {
            Target = target,
            Reason = reason
        };

        return Result.Ok(instance);
    }

    public class CreateChampionRestrictionError() : Error("An error occurred creating a champion restriction instance.");
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public sealed partial class Champion
{
    public Result<Champion> AddRestriction(string target, string reason) =>
        ChampionRestriction.Create(target, reason)
            .OnSuccessTry(Restrictions.Add)
            .Map(_ => this);
}