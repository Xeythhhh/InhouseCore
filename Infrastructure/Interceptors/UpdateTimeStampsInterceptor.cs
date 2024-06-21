using Domain.Primitives;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;
/// <summary>Interceptor for updating timestamps on entities implementing <see cref="IEntity"/> before saving changes.</summary>
public sealed class UpdateTimeStampsInterceptor :
    SaveChangesInterceptor
{
    /// <inheritdoc/>
    public override ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext is null) return base.SavedChangesAsync(eventData, result, cancellationToken);

        foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<IEntity> entity in dbContext.ChangeTracker.Entries<IEntity>())
            entity.Entity.LastUpdatedAt = DateTime.UtcNow;

        dbContext.SaveChanges();

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}