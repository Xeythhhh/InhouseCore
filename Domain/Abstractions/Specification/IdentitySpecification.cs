using System.Linq.Expressions;

namespace Domain.Abstractions.Specification;

/// <summary> Represents a specification that always evaluates to true for any entity of type T. </summary>
/// <typeparam name="T"> The type of entity that this specification applies to. </typeparam>
internal sealed class IdentitySpecification<T> :
    Specification<T>
{
    /// <summary> Converts the specification into a compiled expression that always returns true. </summary>
    /// <returns> An expression that always evaluates to true. </returns>
    public override Expression<Func<T, bool>> ToExpression() => _ => true;
}
