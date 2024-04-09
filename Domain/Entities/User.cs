// TODO: Re-implement user with DuendeIdentityServer Support
//using InhouseCore.Domain.Entities.Abstractions;
//using InhouseCore.Domain.Entities.Users.Members;
//using Microsoft.AspNetCore.Identity;

//namespace InhouseCore.Domain.Entities.Users;

//public sealed class User : IdentityUser<UserId>, IEntity
//{
//    private readonly List<DomainEvent> _domainEvents = new();

//    public Member? Member { get; set; }

//    public IEnumerable<DomainEvent> GetDomainEvents() => _domainEvents.ToList();
//    public void ClearDomainEvents() => _domainEvents.Clear();
//    public void RaiseEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
//}

//public class UserId : IEquatable<UserId>
//{
//    public bool Equals(UserId? other)
//    {
//        throw new NotImplementedException();
//    }
//}
