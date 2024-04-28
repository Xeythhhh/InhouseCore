using System.Linq.Expressions;

namespace Domain.Abstractions.Specification;
internal sealed class OrSpecification<T>(Specification<T> left, Specification<T> right)
    : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var parameter = Expression.Parameter(typeof(T), "entity");

        Expression<Func<T, bool>> leftExpression = left.ToExpression();
        Expression<Func<T, bool>> rightExpression = right.ToExpression();

        BinaryExpression andExpression = Expression.OrElse(
            Expression.Invoke(leftExpression, parameter),
            Expression.Invoke(rightExpression, parameter));

        return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
    }
}
