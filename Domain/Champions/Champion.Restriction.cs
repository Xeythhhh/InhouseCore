using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions;

public sealed partial class Champion
{
    /// <summary>Represents a restriction applied to a champion, which limits its behavior or abilities based on specific criteria.</summary>
    public sealed class Restriction : EntityBase<Restriction.RestrictionId>
    {
        /// <summary>Represents a strongly-typed identifier for <see cref="Restriction"/> instances.</summary>
        /// <param name="Value">The numeric value representing the restriction's unique identifier.</param>
        public sealed record RestrictionId(long Value) : EntityId<Restriction>(Value);

        /// <summary>TODO</summary>
        public Augment Augment { get; set; }
        /// <summary>TODO</summary>
        public Augment? Augment2 { get; set; }
        /// <summary>Gets or sets an optional explanation or rationale for the restriction.</summary>
        public string? Reason { get; set; }
        /// <summary>TODO</summary>
        public bool IsCombo => Augment2 is not null;

        /// <summary>Private constructor for EF Core and internal initialization.</summary>
        private Restriction() { }

        /// <summary>Creates a new restriction with the specified ability name, identifier, color, and reason.</summary>
        /// <param name="reason">Optional reason or explanation for the restriction.</param>
        /// <param name="augment">TODO</param>
        /// <param name="augment2">TODO</param>
        /// <returns> A <see cref="Result{T}"/> containing the created <see cref="Restriction"/> instance if successful, or an error if the creation process fails.</returns>
        public static Result<Restriction> Create(string reason, Augment augment, Augment? augment2 = null)
        {
            try
            {
                return CreateInternal(reason, augment, augment2);
            }
            catch (Exception exception)
            {
                return Result.Fail(new CreateChampionRestrictionError().CausedBy(exception));
            }
        }

        /// <summary>Internal helper method to create a <see cref="Restriction"/> instance.</summary>
        /// <param name="reason">Optional reason or explanation for the restriction.</param>
        /// <param name="augment">TODO</param>
        /// <param name="augment2">TODO</param>
        /// <returns>A <see cref="Restriction"/> instance.</returns>
        private static Restriction CreateInternal(string reason, Augment augment, Augment? augment2 = null)
            => new()
            {
                Augment = augment,
                Augment2 = augment2,
                Reason = reason
            };

        /// <summary>Represents an error that occurs during the creation of a <see cref="Restriction"/>.</summary>
        public class CreateChampionRestrictionError() : Error("An error occurred creating a champion restriction instance.");
    }

    /// <summary> Adds a new restriction to the champion.</summary>
    /// <param name="reason">Optional reason or explanation for the restriction.</param>
    /// <param name="augment">TODO</param>
    /// <param name="augment2">TODO</param>
    /// <returns> A <see cref="Result{Champion}"/> containing the updated <see cref="Champion"/> instance if the restriction is successfully added, or an error result if the addition fails.</returns>
    public Result<Champion> AddRestriction(string reason, Augment augment, Augment? augment2 = null) =>
        Restriction.Create(reason, augment, augment2)
            .Tap(restriction =>
            {
                Restrictions.Add(restriction);
                HasRestrictions = true;
            })
            .Map(_ => this);
}
