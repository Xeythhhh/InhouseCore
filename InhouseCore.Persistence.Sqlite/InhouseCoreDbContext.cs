using InhouseCore.Domain.Entities.Abstract;
using InhouseCore.Domain.Entities.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using InhouseCore.Domain;

namespace InhouseCore.Persistence.Sqlite;

/// <summary>
/// Database Implementation for a combined <see cref="DbContext"/> using ASP.NET Identity and Identity Server with <see cref="Ulid"/> Primary Keys.
/// </summary>
public sealed class InhouseCoreDbContext : 
    IdentityDbContext<User, IdentityRole<Ulid>, Ulid> 
    //,IPersistedGrantDbContext
{
    private readonly ValueConverter<Ulid, string> _ulidToStringConverter;
    //private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

    //public DbSet<PersistedGrant> PersistedGrants { get; set; }
    //public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
    //public DbSet<Key> Keys { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="InhouseCoreDbContext"/>.
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptions{InhouseCoreDbContext}"/>.</param>
    /// <param name="operationalStoreOptions">The <see cref="IOptions{OperationalStoreOptions}"/>.</param>
    /// <param name="ulidToStringConverter">Converter between <see cref="Ulid"/> and <see cref="string"/>.</param>
    public InhouseCoreDbContext(
        DbContextOptions<InhouseCoreDbContext> options,
        //IOptions<OperationalStoreOptions> operationalStoreOptions,
        ValueConverter<Ulid, string> ulidToStringConverter)
        : base(options)
    {
        //_operationalStoreOptions = operationalStoreOptions;
        _ulidToStringConverter = ulidToStringConverter;
    }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        //todo set up logger here
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);

        ////Temporary

        //var entityTypes = DomainAssembly.Value.GetTypes()
        //    .Where(t => typeof(Entity).IsAssignableFrom(t) && 
        //        t.IsClass && 
        //        !t.IsAbstract);

        //foreach (var entityType in entityTypes)
        //    builder.Entity(entityType)
        //        .Property("Id")
        //        .HasConversion(_ulidToStringConverter);

        builder.Entity<User>()
            .Property(u => u.Id)
            .HasConversion(_ulidToStringConverter);

        builder.Entity<IdentityRole<Ulid>>()
            .Property(u => u.Id)
            .HasConversion(_ulidToStringConverter);
    }

    /// <inheritdoc />

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }
    
    /// <inheritdoc />
    //Task<int> IPersistedGrantDbContext.SaveChangesAsync() => SaveChangesAsync();
   
    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is Entity &&
                        x.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added) 
                ((Entity)entity.Entity).CreatedAt = now;
            ((Entity)entity.Entity).LastUpdatedAt = now;
        }
    }
}
