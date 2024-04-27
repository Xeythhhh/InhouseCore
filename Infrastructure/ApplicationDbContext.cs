using System.ComponentModel.DataAnnotations;

using Domain.Entities.Users;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure;
public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    ValueConverter<ApplicationUserId, string> applicationUserIdConverter)
    : IdentityDbContext<ApplicationUser, IdentityRole<ApplicationUserId>, ApplicationUserId>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityRole<ApplicationUserId>>()
            .Property(e => e.Id)
            .HasConversion(applicationUserIdConverter);

        builder.Entity<ApplicationUser>()
            .Property(e => e.Id)
            .HasConversion(applicationUserIdConverter);

        base.OnModelCreating(builder);
    }
}