using System.Linq.Expressions;

namespace Domain.Abstractions.Specification;
internal sealed class AndSpecification<T>(Specification<T> left, Specification<T> right)
    : Specification<T>
    where T : class
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var parameter = Expression.Parameter(typeof(T), "entity");

        Expression<Func<T, bool>> leftExpression = left.ToExpression();
        Expression<Func<T, bool>> rightExpression = right.ToExpression();

        BinaryExpression andExpression = Expression.AndAlso(
            Expression.Invoke(leftExpression, parameter),
            Expression.Invoke(rightExpression, parameter));

        return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
    }
}
