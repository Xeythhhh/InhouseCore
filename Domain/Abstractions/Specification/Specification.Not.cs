using System.Linq.Expressions;

namespace Domain.Abstractions.Specification;

/// <summary> Represents a specification that negates another specification. </summary>
/// <typeparam name="T"> The type of entity that this specification applies to. </typeparam>
internal sealed class NotSpecification<T>(Specification<T> specification) : Specification<T>
{
    /// <summary> Converts the negated specification into an expression that can be used for filtering entities. </summary>
    /// <returns> An expression representing the logical NOT of the specification. </returns>
    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> expression = specification.ToExpression();
        UnaryExpression notExpression = Expression.Not(expression.Body);

        return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
    }
}
