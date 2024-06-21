using Domain.Primitives;

using FluentResults;

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

    public static Result<TestEntity> Create() => Result.Ok(new TestEntity());

    public static Result<TestEntity> CreateWithValue(string something) => string.IsNullOrEmpty(something)
        ? Result.Fail<TestEntity>(new Error("This thing needs a value"))
        : Result.Ok(new TestEntity(something));
}
