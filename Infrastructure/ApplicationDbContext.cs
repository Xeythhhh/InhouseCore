using Application.Abstractions;

using Domain.Champions;
using Domain.ReferenceData;
using Domain.Users;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
    IdentityDbContext<ApplicationUser, ApplicationRole, AspNetIdentityId>(options),
    IUnitOfWork
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Champion> Champions { get; set; }
    public DbSet<Champion.Restriction> ChampionRestrictions { get; set; }
    public DbSet<Champion.Augment> ChampionAugments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(InfrastructureAssembly.Reference);
    }
}