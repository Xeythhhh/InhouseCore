using System.Linq.Expressions;

namespace Domain.Abstractions.Specification;

/// <summary> Represents a base class for defining specifications on entities of type T. </summary>
/// <typeparam name="T"> The type of entity that this specification applies to. </typeparam>
public abstract class Specification<T>
{
    /// <summary> Represents a specification that always evaluates to true for any entity of type T. </summary>
    public static readonly Specification<T> All = new IdentitySpecification<T>();

    /// <summary> Checks if the specification is satisfied by the given entity. </summary>
    /// <param name="entity"> The entity to evaluate against the specification. </param>
    /// <returns> True if the entity satisfies the specification; otherwise, false. </returns>
    public bool IsSatisfiedBy(T entity) => ToExpression().Compile()(entity);

    /// <summary> Converts the specification into an expression that can be used for filtering entities. </summary>
    /// <returns> An expression representing the specification. </returns>
    public abstract Expression<Func<T, bool>> ToExpression();

    /// <summary> Combines this specification with another using a logical AND operation. </summary>
    /// <param name="specification"> The specification to AND with. </param>
    /// <returns> A new specification representing the AND combination of the two specifications. </returns>
    public Specification<T> And(Specification<T> specification) =>
        this == All
            ? specification
            : specification == All
                ? this
                : (Specification<T>)new AndSpecification<T>(this, specification);

    /// <summary> Combines this specification with another using a logical OR operation. </summary>
    /// <param name="specification"> The specification to OR with. </param>
    /// <returns> A new specification representing the OR combination of the two specifications. </returns>
    public Specification<T> Or(Specification<T> specification) =>
        this == All || specification == All
            ? All
            : new OrSpecification<T>(this, specification);

    /// <summary> Negates this specification using a logical NOT operation. </summary>
    /// <returns> A new specification representing the negation of this specification. </returns>
    public Specification<T> Not() => new NotSpecification<T>(this);
}
