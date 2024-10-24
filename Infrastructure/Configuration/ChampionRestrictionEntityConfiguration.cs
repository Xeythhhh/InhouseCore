using Domain.Champions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public sealed class ChampionRestrictionEntityConfiguration :
    EntityConfiguration<Champion.Restriction, Champion.Restriction.RestrictionId>,
    IEntityTypeConfiguration<Champion.Restriction>
{
    new public void Configure(EntityTypeBuilder<Champion.Restriction> builder)
    {
        base.Configure(builder);

        builder.Property(restriction => restriction.ColorHex)
            .HasConversion(
                color => color.Value,
                color => Champion.Restriction.RestrictionColor.Create(color).Value);

        builder.Property(restriction => restriction.Identifier)
            .HasConversion(
                identifier => identifier.Value,
                identifier => Champion.Restriction.RestrictionIdentifier.Create(identifier).Value);
    }
}
