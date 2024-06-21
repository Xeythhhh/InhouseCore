using Domain.Primitives;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;
public sealed class RegisterDomainEventsInterceptor :
    SaveChangesInterceptor
{
    public override ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext is null) return base.SavedChangesAsync(eventData, result, cancellationToken);

        dbContext.ChangeTracker.Entries<IAggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(x =>
            {
                IReadOnlyCollection<IDomainEvent> domainEvents = x.GetDomainEvents();
                x.ClearDomainEvents();
                return domainEvents;
            })
            .Select(x => new OutboxMessage
            {
            });

        Console.WriteLine("Msg from interceptor");

        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}

public sealed record OutboxMessageId : EntityId<OutboxMessage>
{

}

public sealed class OutboxMessage : EntityBase<OutboxMessageId>
{

}