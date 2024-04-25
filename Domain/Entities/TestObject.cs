using Domain.Abstractions;

namespace Domain.Entities;
public class TestObject : Entity<TestObjectId>
{

}

public record TestObjectId(Ulid Value) : IEntityId;
