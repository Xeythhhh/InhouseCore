using CSharpFunctionalExtensions;

using Domain.Entities;
using Domain.Entities.Users;

namespace Domain.UnitTests.TestImplementations;
public class TestEntity : EntityBase<ApplicationUserId>
{
    public string Something { get; set; }

    protected TestEntity(string something)
    {
        Something = something;
    }

    public static Result<TestEntity> Create(string something) => string.IsNullOrEmpty(something)
        ? Result.Failure<TestEntity>("This thing needs something")
        : Result.Success(new TestEntity(something));
}
