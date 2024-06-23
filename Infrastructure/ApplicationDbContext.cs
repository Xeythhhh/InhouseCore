using Application.Abstractions;

using Domain.Champions;
using Domain.Champions.ValueObjects;
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
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(InfrastructureAssembly.Reference);

        //builder.Entity<ChampionName>().HasNoKey();
        //builder.Entity<ChampionRole>().HasNoKey();

    }
}