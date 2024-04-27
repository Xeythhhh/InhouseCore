using Domain.Entities.Users;

using Infrastructure.Converters.Ids;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IdConverters idConverters)
    : IdentityDbContext<ApplicationUser, ApplicationRole, ApplicationUserId>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationRole>()
            .Property(e => e.Id)
            .HasConversion(idConverters[typeof(ApplicationRole)]);

        builder.Entity<ApplicationUser>()
            .Property(e => e.Id)
            .HasConversion(idConverters[typeof(ApplicationUser)]);

        base.OnModelCreating(builder);
    }
}