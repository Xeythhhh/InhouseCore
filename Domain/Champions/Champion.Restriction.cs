using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions;

public sealed partial class Champion
{
    /// <summary>Represents a restriction applied to a champion, which limits its behavior or abilities based on specific criteria.</summary>
    public sealed partial class Restriction : EntityBase<Restriction.RestrictionId>
    {
        /// <summary>Represents a strongly-typed identifier for <see cref="Restriction"/> instances.</summary>
        /// <param name="Value">The numeric value representing the restriction's unique identifier.</param>
        public sealed record RestrictionId(long Value) : EntityId<Restriction>(Value);

        /// <summary>Gets or sets the name of the ability that this restriction applies to.</summary>
        public string AbilityName { get; set; }

        /// <summary>Gets or sets the identifier that specifies the type or category of the restriction.</summary>
        public RestrictionIdentifier Identifier { get; set; }

        /// <summary>Gets or sets the hex color code or known color name associated with this restriction.</summary>
        public RestrictionColor ColorHex { get; set; }

        /// <summary>Gets or sets an optional explanation or rationale for the restriction.</summary>
        public string? Reason { get; set; }

        /// <summary>Private constructor for EF Core and internal initialization.</summary>
        private Restriction() { }

        /// <summary>Creates a new restriction with the specified ability name, identifier, color, and reason.</summary>
        /// <param name="name">The name of the ability this restriction applies to.</param>
        /// <param name="identifier">The identifier that specifies the type of restriction.</param>
        /// <param name="hexOrColorName">A valid hex color code or known color name representing the restriction.</param>
        /// <param name="reason">Optional reason or explanation for the restriction.</param>
        /// <returns> A <see cref="Result{T}"/> containing the created <see cref="Restriction"/> instance if successful, or an error if the creation process fails.</returns>
        public static Result<Restriction> Create(string name, string identifier, string hexOrColorName, string reason)
        {
            try
            {
                return CreateInternal(name, identifier, hexOrColorName, reason);
            }
            catch (Exception exception)
            {
                return Result.Fail(new CreateChampionRestrictionError().CausedBy(exception));
            }
        }

        /// <summary>Internal helper method to create a <see cref="Restriction"/> instance.</summary>
        /// <param name="name">The name of the ability this restriction applies to.</param>
        /// <param name="identifier">The identifier that specifies the type of restriction.</param>
        /// <param name="color">A valid hex color code or known color name representing the restriction.</param>
        /// <param name="reason">Optional reason or explanation for the restriction.</param>
        /// <returns>A <see cref="Restriction"/> instance.</returns>
        private static Restriction CreateInternal(string name, string identifier, string color, string reason)
            => new()
            {
                AbilityName = name,
                Identifier = identifier,
                ColorHex = color,
                Reason = reason
            };

        /// <summary>Represents an error that occurs during the creation of a <see cref="Restriction"/>.</summary>
        public class CreateChampionRestrictionError() : Error("An error occurred creating a champion restriction instance.");
    }

    /// <summary> Adds a new restriction to the champion.</summary>
    /// <param name="name">The name of the ability this restriction applies to.</param>
    /// <param name="identifier">The identifier that specifies the type of restriction.</param>
    /// <param name="color">A valid hex color code or known color name representing the restriction.</param>
    /// <param name="reason">Optional reason or explanation for the restriction.</param>
    /// <returns> A <see cref="Result{Champion}"/> containing the updated <see cref="Champion"/> instance if the restriction is successfully added, or an error result if the addition fails.</returns>
    public Result<Champion> AddRestriction(string name, string identifier, string color, string reason) =>
        Restriction.Create(name, identifier, color, reason)
            .OnSuccessTry(restriction =>
            {
                Restrictions.Add(restriction);
                HasRestrictions = true;
            })
            .Map(_ => this);
}
