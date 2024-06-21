using Domain.Primitives;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        Console.WriteLine("sakdjhsakjdakjdahsdjksa");

        DbContext? dbContext = eventData.Context;
        if (dbContext is null) return base.SavedChangesAsync(eventData, result, cancellationToken);

        foreach (EntityEntry<IEntity> entity in dbContext.ChangeTracker.Entries<IEntity>())
            entity.Entity.LastUpdatedAt = DateTime.UtcNow;

        dbContext.SaveChanges();

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}