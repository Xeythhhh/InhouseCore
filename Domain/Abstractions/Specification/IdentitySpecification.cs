using System.Linq.Expressions;

namespace Domain.Abstractions.Specification;
internal sealed class IdentitySpecification<T> : Specification<T> where T : class
{
    public override Expression<Func<T, bool>> ToExpression() => x => true;
}