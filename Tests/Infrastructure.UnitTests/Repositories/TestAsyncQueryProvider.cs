using Microsoft.EntityFrameworkCore.Query;

using System.Linq.Expressions;

namespace Infrastructure.UnitTests.Repositories;

public class TestAsyncQueryProvider<TEntity> :
    IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;
    internal TestAsyncQueryProvider(IQueryProvider inner) => _inner = inner;
    public IQueryable CreateQuery(Expression expression) => new TestAsyncEnumerable<TEntity>(expression);
    public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new TestAsyncEnumerable<TElement>(expression);
    public object Execute(Expression expression) => _inner.Execute(expression)!;
    public TResult Execute<TResult>(Expression expression) => _inner.Execute<TResult>(expression);
    public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression) => new TestAsyncEnumerable<TResult>(expression);
#pragma warning disable RCS1047 // Non-asynchronous method name should not end with 'Async'
    TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) => Execute<TResult>(expression);
#pragma warning restore RCS1047 // Non-asynchronous method name should not end with 'Async'
}
