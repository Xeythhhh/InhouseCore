namespace Domain.Entities.Users;
/// <summary>
/// Strongly-typed Id for <see cref="ApplicationUser"/>
/// </summary>
public record ApplicationUserId(Ulid Value) : IEntityId;
