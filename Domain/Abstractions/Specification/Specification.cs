using System.Linq.Expressions;

namespace Domain.Abstractions.Specification;
public abstract class Specification<T>
{
    public static readonly Specification<T> All = new IdentitySpecification<T>();

    public bool IsSatisfiedBy(T entity) => ToExpression().Compile()(entity);

    public abstract Expression<Func<T, bool>> ToExpression();

    public Specification<T> And(Specification<T> specification) => this == All
        ? specification : specification == All
            ? this : (Specification<T>)new AndSpecification<T>(this, specification);

    public Specification<T> Or(Specification<T> specification) => this == All || specification == All
        ? All : new OrSpecification<T>(this, specification);

    public Specification<T> Not() => new NotSpecification<T>(this);
}