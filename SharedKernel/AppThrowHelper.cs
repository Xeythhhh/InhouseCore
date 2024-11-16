using System.Linq.Expressions;

namespace SharedKernel;

public static class AppThrowHelper
{
    public static void ThrowIfAnyAreNull(params Expression<Func<object?>>[] args)
    {
        foreach (Expression<Func<object?>> argExpression in args)
        {
            object? arg = argExpression.Compile().Invoke();
            ArgumentNullException.ThrowIfNull(arg, GetParameterName(argExpression));
        }
    }

    public static void ThrowIfAnyAreNullOrWhitespace(params Expression<Func<string?>>[] args)
    {
        foreach (Expression<Func<string?>> argExpression in args)
        {
            string? arg = argExpression.Compile().Invoke();
            ArgumentException.ThrowIfNullOrWhiteSpace(arg, GetParameterName(argExpression));
        }
    }

    private static string GetParameterName(Expression<Func<object?>> expression) =>
        expression.Body is MemberExpression memberExpression
        ? memberExpression.Member.Name
        : expression.ToString();

    private static string GetParameterName(Expression<Func<string?>> expression) =>
        expression.Body is MemberExpression memberExpression
            ? memberExpression.Member.Name
            : expression.ToString();
}
