using SharedKernel.Primitives.Result;

namespace SharedKernel.UnitTests.Primitives.Results.Methods;

public class ResultEnsureTests : VerifyBaseTest
{
    [Fact]
    public Task ShouldPassEnsureWithSynchronousPredicate() =>
        Verify(Result.Ok().Ensure(() => true, "Error Message"), Settings);

    [Fact]
    public Task ShouldFailEnsureWithSynchronousPredicate() =>
        Verify(Result.Ok().Ensure(() => false, "Error Message"), Settings);

    [Fact]
    public Task ShouldHandleAsyncEnsureWithTruePredicate() =>
        Verify(Result.Ok().Ensure(() => Task.FromResult(true), "Error Message"), Settings);

    [Fact]
    public Task ShouldHandleAsyncEnsureWithFalsePredicate() =>
        Verify(Result.Ok().Ensure(() => Task.FromResult(false), "Error Message"), Settings);

    [Fact]
    public Task ShouldHandleValueTaskEnsureWithTruePredicate() =>
        Verify(Result.Ok().Ensure(() => new ValueTask<bool>(true), "Error Message"), Settings);

    [Fact]
    public Task ShouldHandleValueTaskEnsureWithFalsePredicate() =>
        Verify(Result.Ok().Ensure(() => new ValueTask<bool>(false), "Error Message"), Settings);

    [Fact]
    public Task ShouldPassEnsureWithResultPredicate() =>
        Verify(Result.Ok().Ensure(() => Result.Ok()), Settings);

    [Fact]
    public Task ShouldFailEnsureWithResultPredicate() =>
        Verify(Result.Ok().Ensure(() => Result.Fail("Inner Error Message")), Settings);

    [Fact]
    public Task ShouldPassEnsureWithGenericResultPredicate() =>
        Verify(Result.Ok().Ensure(() => Result.Ok("Inner Ok Result")), Settings);

    [Fact]
    public Task ShouldFailEnsureWithGenericResultPredicate() =>
        Verify(Result.Ok().Ensure(() => Result.Fail<string>("Inner Failure Error Message")), Settings);

    [Fact]
    public Task ShouldHandleAsyncEnsureWithResultPredicate() =>
        Verify(Result.Ok().Ensure(() => Task.FromResult(Result.Ok())), Settings);

    [Fact]
    public Task ShouldHandleAsyncEnsureWithFailedResultPredicate() =>
        Verify(Result.Ok().Ensure(() => Task.FromResult(Result.Fail("Async Inner Failure Error Message"))), Settings);

    [Fact]
    public Task ShouldHandleValueTaskEnsureWithResultPredicate() =>
        Verify(Result.Ok().Ensure(() => new ValueTask<Result>(Result.Ok())), Settings);

    [Fact]
    public Task ShouldHandleValueTaskEnsureWithFailedResultPredicate() =>
        Verify(Result.Ok().Ensure(() => new ValueTask<Result>(Result.Fail("ValueTask Inner Failure Error Message"))), Settings);

    [Fact]
    public Task ShouldFailEnsureWithNullSynchronousPredicate() =>
        Throws(() => Result.Ok().Ensure((Func<bool>)null!, "Error Message"), Settings)
            .IgnoreStackTrace();

    [Fact]
    public Task ShouldFailEnsureWithNullAsyncPredicate() =>
        Verify(Assert.ThrowsAsync<NullReferenceException>(() =>
            Result.Ok().Ensure((Func<Task<bool>>)null!, "Error Message")), Settings);

    [Fact]
    public Task ShouldFailEnsureWithNullValueTaskPredicate() =>
        Verify(Assert.ThrowsAsync<NullReferenceException>(async () =>
            await Result.Ok().Ensure(null!, "Error Message")), Settings);

    [Fact]
    public Task ShouldFailEnsureWithNullResultPredicate() =>
        Throws(() => Result.Ok().Ensure((Func<Result>)null!), Settings)
            .IgnoreStackTrace();

    [Fact]
    public Task ShouldFailEnsureWithNullGenericResultPredicate() =>
        Throws(() => Result.Ok().Ensure((Func<Result<string>>)null!), Settings)
            .IgnoreStackTrace();

    [Fact]
    public Task ShouldFailEnsureWithNullAsyncResultPredicate() =>
        Verify(Assert.ThrowsAsync<NullReferenceException>(() =>
            Result.Ok().Ensure((Func<Task<Result>>)null!)), Settings);

    [Fact]
    public Task ShouldFailEnsureWithNullGenericAsyncResultPredicate() =>
        Verify(Assert.ThrowsAsync<NullReferenceException>(() =>
            Result.Ok().Ensure((Func<Task<Result<string>>>)null!)), Settings);

    [Fact]
    public Task ShouldFailEnsureWithNullValueTaskResultPredicate() =>
        Verify(Assert.ThrowsAsync<NullReferenceException>(async () =>
            await Result.Ok().Ensure(null!)), Settings);

    [Fact]
    public Task ShouldFailEnsureWithNullGenericValueTaskResultPredicate() =>
        Verify(Assert.ThrowsAsync<NullReferenceException>(async () =>
            await Result.Ok().Ensure((Func<ValueTask<Result<string>>>)null!)), Settings);
}
