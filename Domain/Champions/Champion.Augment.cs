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

        public string Icon { get; set; }

        /// <summary>Private constructor for EF Core and internal initialization.</summary>
        private Augment() { }

        /// <summary>Creates a new augment with the specified ability name, target, color, and reason.</summary>
        /// <param name="name">The name of the ability this augment applies to.</param>
        /// <param name="target">The target that specifies the type of augment.</param>
        /// <param name="hexOrColorName">A valid hex color code or known color name representing the augment.</param>
        /// <returns> A <see cref="Result{T}"/> containing the created <see cref="Augment"/> instance if successful, or an error if the creation process fails.</returns>
        public static Result<Augment> Create(string name, string target, string hexOrColorName, string icon)
        {
            try
            {
                return CreateInternal(name, target, hexOrColorName, icon);
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
        private static Augment CreateInternal(string name, string target, string color, string icon)
            => new()
            {
                Name = name,
                Target = target,
                ColorHex = color,
                Icon = icon
            };

        /// <summary>Represents an error that occurs during the creation of a <see cref="Augment"/>.</summary>
        public class CreateChampionAugmentError() : Error("An error occurred creating a champion augment instance.");
    }

    /// <summary> Adds a new <see cref="Augment"/> to the <see cref="Champion"/>.</summary>
    /// <param name="name">The name of the <see cref="Augment"/> to add.</param>
    /// <param name="target">The target for the <see cref="Augment"/> (e.g., the area or entity it applies to).</param>
    /// <param name="color">The color representing the <see cref="Augment"/>, typically as a hex code.</param>
    /// <param name="icon">The icon identifier or path for the <see cref="Augment"/>.</param>
    /// <returns> A <see cref="Result{Champion}"/> containing the updated <see cref="Champion"/> instance if the <see cref="Augment"/> 
    /// is successfully added, or an <see cref="Augment.CreateChampionAugmentError"/> result if the addition fails. </returns>
    /// <remarks> This method creates an <see cref="Augment"/> using the provided parameters. If the <see cref="Augment"/> is successfully created, 
    /// it is added to the <see cref="Champion"/>'s list of <see cref="Augment"/>s, and the updated <see cref="Champion"/> is returned. 
    /// Otherwise, the result contains an <see cref="Augment.CreateChampionAugmentError"/>.</remarks>
    public Result<Champion> AddAugment(
        string name,
        string target,
        string color,
        string icon) =>
        Augment.Create(name, target, color, icon)
            .Tap(Augments.Add)
            .Map(_ => this);
}
