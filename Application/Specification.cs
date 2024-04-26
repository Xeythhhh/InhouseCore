using System.Linq.Expressions;

namespace Application;
public abstract class Specification<T>
    where T : class
{
    public bool IsSatisfiedBy(T entity)
    {
        Func<T, bool> predicate = ToExpression().Compile();
        return predicate(entity);
    }

    public abstract Expression<Func<T, bool>> ToExpression();
}