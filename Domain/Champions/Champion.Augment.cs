using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions;

public sealed partial class Champion
{
    /// <summary>Represents an augment that you can use to enhance your champion.</summary>
    public sealed partial class Augment : EntityBase<Augment.AugmentId>
    {
        /// <summary>Represents a strongly-typed target for <see cref="Augment"/> instances.</summary>
        /// <param name="Value">The numeric value representing the Augment's unique target.</param>
        public sealed record AugmentId(long Value) : EntityId<Augment>(Value);

        /// <summary>Gets or sets the name of the Augment.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the target of the Augment.</summary>
        public AugmentTarget Target { get; set; }

        /// <summary>Gets or sets the hex color code or known color name associated with this Augment.</summary>
        public AugmentColor ColorHex { get; set; }

        /// <summary>Private constructor for EF Core and internal initialization.</summary>
        private Augment() { }

        /// <summary>Creates a new augment with the specified ability name, target, color, and reason.</summary>
        /// <param name="name">The name of the ability this augment applies to.</param>
        /// <param name="target">The target that specifies the type of augment.</param>
        /// <param name="hexOrColorName">A valid hex color code or known color name representing the augment.</param>
        /// <returns> A <see cref="Result{T}"/> containing the created <see cref="Augment"/> instance if successful, or an error if the creation process fails.</returns>
        public static Result<Augment> Create(string name, string target, string hexOrColorName)
        {
            try
            {
                return CreateInternal(name, target, hexOrColorName);
            }
            catch (Exception exception)
            {
                return Result.Fail(new CreateChampionAugmentError().CausedBy(exception));
            }
        }

        /// <summary>Internal helper method to create a <see cref="Augment"/> instance.</summary>
        /// <param name="name">The name of the ability this augment applies to.</param>
        /// <param name="target">The target that specifies the type of augment.</param>
        /// <param name="color">A valid hex color code or known color name representing the augment.</param>
        /// <returns>A <see cref="Augment"/> instance.</returns>
        private static Augment CreateInternal(string name, string target, string color)
            => new()
            {
                Name = name,
                Target = target,
                ColorHex = color
            };

        /// <summary>Represents an error that occurs during the creation of a <see cref="Augment"/>.</summary>
        public class CreateChampionAugmentError() : Error("An error occurred creating a champion augment instance.");
    }

    /// <summary> Adds a new augment to the champion.</summary>
    /// <param name="name">TODO</param>
    /// <param name="target">TODO</param>
    /// <param name="color">TODO</param>
    /// <returns> A <see cref="Result{Champion}"/> containing the updated <see cref="Champion"/> instance if the augment is successfully added, or an error result if the addition fails.</returns>
    public Result<Champion> AddAugment(string name, string target, string color) =>
        Augment.Create(name, target, color)
            .Tap(Augments.Add)
            .Map(_ => this);
}
