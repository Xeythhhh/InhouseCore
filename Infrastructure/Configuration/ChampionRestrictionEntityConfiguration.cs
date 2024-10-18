using Domain.Champions;
using Domain.Champions.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public sealed class ChampionRestrictionEntityConfiguration :
    EntityConfiguration<ChampionRestriction, ChampionRestriction.RestrictionId>,
    IEntityTypeConfiguration<ChampionRestriction>
{
    new public void Configure(EntityTypeBuilder<ChampionRestriction> builder) => base.Configure(builder);
}
