using CSharpFunctionalExtensions;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users;

public sealed class ApplicationRole
    : IdentityRole<AspNetIdentityId>, IEntity<AspNetIdentityId>
{
    private readonly List<DomainEvent> _domainEvents = new();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; }

    private ApplicationRole()
    {
        // Private constructor required by EF Core and auto-mappings
    }

    public IEnumerable<DomainEvent> GetDomainEvents() => _domainEvents.ToList();
    public void ClearDomainEvents() => _domainEvents.Clear();
    public void RaiseEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public static Result<ApplicationRole> Create(string name)
    {
        name = name?.Trim() ?? string.Empty;
        ApplicationRole instance = new()
        {
            Name = name
        };

        ValidationResult validationResult = Validator.Instance.Validate(instance);
        return validationResult.IsValid
            ? Result.Success(instance)
            : Result.Failure<ApplicationRole>(string.Join(", ",
                validationResult.Errors.Select(e => e.ErrorMessage)));
    }

    private class Validator : AbstractValidator<ApplicationRole>
    {
        public static Validator Instance = new();
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}