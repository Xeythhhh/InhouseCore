using Domain.Abstractions;

using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

/// <summary>
/// Strongly-typed Id for <see cref="ApplicationUser"/>
/// </summary>
public record ApplicationUserId(Ulid Value) : IEntityId;

/// <summary>
/// The User
/// </summary>
public class ApplicationUser : IdentityUser<ApplicationUserId>, IEntity<ApplicationUserId>
{
    private readonly List<DomainEvent> _domainEvents = new();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; }

    public IEnumerable<DomainEvent> GetDomainEvents() => _domainEvents.ToList();
    public void ClearDomainEvents() => _domainEvents.Clear();
    public void RaiseEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}