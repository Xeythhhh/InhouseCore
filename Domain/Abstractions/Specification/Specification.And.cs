using System.Linq.Expressions;

namespace Domain.Abstractions.Specification;

/// <summary> Represents a specification that combines two specifications with a logical AND. </summary>
/// <typeparam name="T"> The type of entity that this specification applies to. </typeparam>
internal sealed class AndSpecification<T>(Specification<T> left, Specification<T> right) :
    Specification<T>
{
    /// <summary> Converts the combined specifications into an expression that can be used for filtering entities. </summary>
    /// <returns> An expression representing the logical AND of the two specifications. </returns>
    public override Expression<Func<T, bool>> ToExpression()
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), "entity");

        Expression<Func<T, bool>> leftExpression = left.ToExpression();
        Expression<Func<T, bool>> rightExpression = right.ToExpression();

        BinaryExpression andExpression = Expression.AndAlso(
            Expression.Invoke(leftExpression, parameter),
            Expression.Invoke(rightExpression, parameter));

        return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
    }
}
