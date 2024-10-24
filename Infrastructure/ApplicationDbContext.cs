using Application.Abstractions;

using Domain.Champions;
using Domain.Users;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
    IdentityDbContext<ApplicationUser, ApplicationRole, AspNetIdentityId>(options),
    IUnitOfWork
{
    public DbSet<Champion> Champions { get; set; }
    public DbSet<ChampionRestriction> ChampionRestrictions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(InfrastructureAssembly.Reference);
    }
}