namespace Infrastructure.UnitTests.Repositories;
public class TestAsyncEnumerator<T>(IEnumerator<T> inner) :
    IAsyncEnumerator<T>
{
    public ValueTask<bool> MoveNextAsync() => new(inner.MoveNext());
    public T Current => inner.Current;
    public ValueTask DisposeAsync()
    {
        inner.Dispose();
        GC.SuppressFinalize(this);
        return new ValueTask();
    }
}