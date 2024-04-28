namespace Domain.Entities;
public interface IEntity<TEntityId>
    where TEntityId : IEntityId
{
    public TEntityId Id { get; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastUpdatedAt { get; set; }

    public IEnumerable<DomainEvent> GetDomainEvents();
    public void ClearDomainEvents();
    public void RaiseEvent(DomainEvent domainEvent);
}
