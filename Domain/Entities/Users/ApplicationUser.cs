using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users;

/// <summary>Represents an application user with a strongly-typed identifier and domain event handling capabilities.</summary>
public sealed class ApplicationUser :
    IdentityUser<AspNetIdentityId>,
    IEntity<AspNetIdentityId>
{
    /// <summary>A list of domain events associated with the application user.</summary>
    private readonly List<DomainEvent> _domainEvents = new();

    /// <summary>Gets the timestamp of when the user was created.</summary>
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    /// <summary>Gets or sets the timestamp of when the user was last updated.</summary>
    public DateTime LastUpdatedAt { get; set; }

    /// <summary>Private constructor required by EF Core and auto-mappings.</summary>
    private ApplicationUser() { }

    /// <summary>Gets the domain events associated with the user.</summary>
    /// <returns>A collection of domain events.</returns>
    public IEnumerable<DomainEvent> GetDomainEvents() => _domainEvents.ToList();

    /// <summary>Clears the domain events associated with the user.</summary>
    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>Raises a new domain event.</summary>
    /// <param name="domainEvent">The domain event to raise.</param>
    public void RaiseEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
