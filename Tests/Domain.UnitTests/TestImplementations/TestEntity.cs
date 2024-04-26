using CSharpFunctionalExtensions;

using Domain.Entities;

namespace Domain.UnitTests.TestImplementations;
public class TestEntity : EntityBase<TestEntityId>
{
    public string Something { get; set; }

    protected TestEntity(string something)
    {
        Something = something;
    }

    public static Result<TestEntity> Create(string something)
    {
        if (string.IsNullOrEmpty(something))
            return Result.Failure<TestEntity>("This thing needs something");

        return Result.Success(new TestEntity(something));
    }
}

public record TestEntityId(Ulid Value) : EntityId<TestEntityId>(Value);
