using InhouseCore.Domain.Entities.Abstract;

using Microsoft.AspNetCore.Identity;

namespace InhouseCore.Domain.Entities.Identity;

/// <summary>
/// Application User with Identity
/// </summary>
public sealed class User : IdentityUser<Ulid>, IEntity
{
    public User()
    {
        Id = Ulid.NewUlid();
    }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; }
}
