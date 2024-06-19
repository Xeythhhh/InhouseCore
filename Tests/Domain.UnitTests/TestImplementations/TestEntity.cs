using CSharpFunctionalExtensions;

using Domain.Entities;
using Domain.Entities.Users;

namespace Domain.UnitTests.TestImplementations;

public sealed record TestEntityId(long Value) :
    EntityId<TestEntity>(Value);

public sealed class TestEntity :
    EntityBase<TestEntityId>
{
    public string? Something { get; set; }

    private TestEntity() { }
    private TestEntity(string something)
    {
        Something = something;
    }

    public static Result<TestEntity> Create() => Result.Success(new TestEntity());

    public static Result<TestEntity> CreateWithValue(string something) => string.IsNullOrEmpty(something)
        ? Result.Failure<TestEntity>("This thing needs something")
        : Result.Success(new TestEntity(something));
}
