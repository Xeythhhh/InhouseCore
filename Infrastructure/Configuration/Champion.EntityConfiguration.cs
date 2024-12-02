using Domain.Champions;
using Domain.ReferenceData;

using Infrastructure.Identifiers;

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

        builder.Property(e => e.GameId)
            .HasConversion(Id.GetValueConverter<Game.GameId>());

        builder.Property(champion => champion.Name)
            .HasConversion(
                name => name.Value,
                name => Champion.ChampionName.Create(name).Value);

        builder.Property(champion => champion.Role)
            .HasConversion(
                role => role.Value,
                role => Champion.ChampionRole.Create(role, null).Value);

        builder.Property(champion => champion.Avatar)
            .HasConversion(
                avatar => avatar.Value,
                avatar => Champion.ChampionAvatar.Create(avatar).Value);

        builder.HasMany(champion => champion.Restrictions);
    }
}
