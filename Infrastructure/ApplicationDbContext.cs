using Domain.Entities.Users;

using Infrastructure.Identifiers;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IdConverters idConverters)
    : IdentityDbContext<ApplicationUser, ApplicationRole, ApplicationUserId>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //TODO key for all entities with reflection or with a base entityBuilder
        builder.Entity<ApplicationUser>()
            .HasKey(e => e.Id);

        builder.Entity<ApplicationRole>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(idConverters[typeof(ApplicationUser)])
            .HasValueGenerator<IdValueGenerator>();

        builder.Entity<ApplicationUser>()
            .Property(e => e.Id)
            .HasConversion(idConverters[typeof(ApplicationUser)]);

        base.OnModelCreating(builder);
    }
}
