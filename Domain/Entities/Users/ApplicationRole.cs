using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users;

// Used ApplicationUserId as a strongly-typed id due to identity limitations :/
// Might implement own IdentityDbContext
public sealed class ApplicationRole
    : IdentityRole<ApplicationUserId>, IEntity<ApplicationUserId>
{
    private readonly List<DomainEvent> _domainEvents = new();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; }

    public IEnumerable<DomainEvent> GetDomainEvents() => _domainEvents.ToList();
    public void ClearDomainEvents() => _domainEvents.Clear();
    public void RaiseEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}