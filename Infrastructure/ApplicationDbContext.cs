using Application.Abstractions;

using Domain.Champions;
using Domain.Users;

using Infrastructure.Configuration;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
    IdentityDbContext<ApplicationUser, ApplicationRole, AspNetIdentityId>(options),
    IUnitOfWork
{
    public DbSet<Champion> Champions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        builder.ApplyConfiguration(new ApplicationRoleEntityConfiguration());
        builder.ApplyConfiguration(new ChampionEntityConfiguration());

        base.OnModelCreating(builder);
    }
}