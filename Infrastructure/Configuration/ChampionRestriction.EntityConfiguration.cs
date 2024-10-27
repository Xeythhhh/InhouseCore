using Domain.Champions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public sealed class ChampionRestrictionEntityConfiguration :
    EntityConfiguration<Champion.Restriction, Champion.Restriction.RestrictionId>,
    IEntityTypeConfiguration<Champion.Restriction>
{
    new public void Configure(EntityTypeBuilder<Champion.Restriction> builder) => base.Configure(builder);
}
