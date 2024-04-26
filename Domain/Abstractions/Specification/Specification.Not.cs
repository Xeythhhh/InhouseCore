using System.Linq.Expressions;

namespace Domain.Abstractions.Specification;
internal sealed class NotSpecification<T>(Specification<T> specification)
    : Specification<T>
    where T : class
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> expression = specification.ToExpression();
        UnaryExpression notExpression = Expression.Not(expression.Body);

        return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
    }
}