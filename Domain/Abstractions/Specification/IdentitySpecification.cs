using System.Linq.Expressions;

namespace Domain.Abstractions.Specification;
internal sealed class IdentitySpecification<T>
    : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression() => _ => true;
}