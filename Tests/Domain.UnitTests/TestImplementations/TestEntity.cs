using Domain.Primitives;
using Domain.Primitives.Result;

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
        ? Result.Failure<TestEntity>(new Error("UnitTest.TestEntity", "This thing needs a value"))
        : Result.Success(new TestEntity(something));
}
