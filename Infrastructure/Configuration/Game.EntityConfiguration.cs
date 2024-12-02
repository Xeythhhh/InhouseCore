using Domain.ReferenceData;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public sealed class GameEntityConfiguration :
    EntityConfiguration<Game, Game.GameId>,
    IEntityTypeConfiguration<Game>
{
    new public void Configure(EntityTypeBuilder<Game> builder)
    {
        base.Configure(builder);

#pragma warning disable CS8604 // Possible null reference argument.
        builder.Property(game => game.Roles)
            .HasConversion(
                roles => string.Join(";", roles),
                roles => roles.Split(";", StringSplitOptions.None))
            .Metadata.SetValueComparer(new ValueComparer<string[]>(
                (array1, array2) => array1.SequenceEqual(array2),
                array => array.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
                array => array.ToArray()
            ));

        builder.Property(game => game.Augments)
            .HasConversion(
                augments => string.Join(";", augments),
                augments => augments.Split(";", StringSplitOptions.None))
            .Metadata.SetValueComparer(new ValueComparer<string[]>(
                (array1, array2) => array1.SequenceEqual(array2),
                array => array.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
                array => array.ToArray()
            ));

        builder.Property(game => game.AugmentColors)
            .HasConversion(
                augmentColors => string.Join(";", augmentColors),
                augmentColors => augmentColors.Split(";", StringSplitOptions.None))
            .Metadata.SetValueComparer(new ValueComparer<string[]>(
                (array1, array2) => array1.SequenceEqual(array2),
                array => array.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
                array => array.ToArray()
            ));
#pragma warning restore CS8604 // Possible null reference argument.

    }
}
