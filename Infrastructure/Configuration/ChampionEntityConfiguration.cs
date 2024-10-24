using Domain.Champions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public sealed class ChampionEntityConfiguration :
    EntityConfiguration<Champion, Champion.ChampionId>,
    IEntityTypeConfiguration<Champion>
{
    new public void Configure(EntityTypeBuilder<Champion> builder)
    {
        base.Configure(builder);

        builder.Property(champion => champion.Name)
            .HasConversion(
                name => name.Value,
                name => Champion.ChampionName.Create(name).Value);

        builder.Property(champion => champion.Role)
            .HasConversion(
                role => role.Value,
                role => Champion.ChampionRole.Create(role).Value);

        builder.HasMany(champion => champion.Restrictions);
    }
}
