using Domain.Entities;

namespace Domain.UnitTests.TestImplementations;

public record TestEntityId(Ulid Value) : EntityId<TestEntityId>(Value);
