using System.Linq.Expressions;

using Domain.Abstractions.Specification;

namespace Domain.UnitTests.TestImplementations;
public sealed class TestSpecification(string testValue) : Specification<TestEntity>
{
    public override Expression<Func<TestEntity, bool>> ToExpression()
    {
        return entity => entity.Something.Contains(testValue);
    }
}