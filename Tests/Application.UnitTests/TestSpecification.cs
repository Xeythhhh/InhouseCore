using System.Linq.Expressions;

using Domain.UnitTests;

namespace Application.UnitTests;
public sealed class TestSpecification1 : Specification<TestEntity>
{
    private const string _somethingTestConstant = "Test Specification 1";

    public override Expression<Func<TestEntity, bool>> ToExpression()
    {
        return entity => entity.Something == _somethingTestConstant;
    }
}

public sealed class TestSpecification2 : Specification<TestEntity>
{
    private const string _somethingTestConstant = "Test Specification 2";

    public override Expression<Func<TestEntity, bool>> ToExpression()
    {
        return entity => entity.Something == _somethingTestConstant;
    }
}
