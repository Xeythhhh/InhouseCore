using Domain.Entities.Users;

using Infrastructure.Identifiers;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, AspNetIdentityId>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //TODO key for all entities with reflection or with a base entityBuilder
        builder.Entity<ApplicationUser>()
            .HasKey(e => e.Id);

        builder.Entity<ApplicationRole>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(Id.ValueConverters[typeof(ApplicationRole)])
            .HasValueGenerator<IdValueGenerator>();

        builder.Entity<ApplicationUser>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(Id.ValueConverters[typeof(ApplicationUser)])
            .HasValueGenerator<IdValueGenerator>();

        base.OnModelCreating(builder);
    }
}