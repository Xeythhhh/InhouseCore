namespace Domain.Entities.Users;
/// <summary>Strongly-typed Id for <see cref="ApplicationUser"/></summary>
public sealed record ApplicationUserId(Ulid Value) : EntityId<ApplicationUser>(Value);