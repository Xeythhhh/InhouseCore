using Domain.Champions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public sealed class ChampionAugmentEntityConfiguration :
    EntityConfiguration<Champion.Augment, Champion.Augment.AugmentId>,
    IEntityTypeConfiguration<Champion.Augment>
{
    new public void Configure(EntityTypeBuilder<Champion.Augment> builder)
    {
        base.Configure(builder);

        builder.Property(restriction => restriction.ColorHex)
            .HasConversion(
                color => color.Value,
                color => Champion.Augment.AugmentColor.Create(color).Value);

        builder.Property(restriction => restriction.Target)
            .HasConversion(
                target => target.Value,
                target => Champion.Augment.AugmentTarget.Create(target).Value);
    }
}
