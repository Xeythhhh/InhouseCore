using SharedKernel.Primitives.Result;

namespace SharedKernel.UnitTests.Primitives.Results.Methods;

public class ResultTBindTests : VerifyBaseTest
{
    [Fact]
    public Task ShouldBindSuccessToAnotherSuccess() =>
        Verify(Result.Ok("Initial Ok Result")
            .Bind(value => Result.Ok($"Bound: {value}")), Settings);

    [Fact]
    public Task ShouldBindFailureToAnotherResult() =>
        Verify(Result.Fail<string>("Initial Failure Result")
            .Bind(value => Result.Ok($"Bound: {value}")), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    public Task ShouldBindSuccessToFailure() =>
        Verify(Result.Ok("Initial Ok Result")
            .Bind(_ => Result.Fail<string>("Binding Failure")), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    public Task ShouldPropagateReasonsWhenBindingSuccessToSuccess() =>
        Verify(Result.Ok("Initial Ok Result")
            .WithSuccess("Success 1")
            .Bind(value => Result.Ok($"Bound: {value}").WithSuccess("Success 2")), Settings);

    [Fact]
    public Task ShouldHandleAsyncBindingFromSuccessToSuccess() =>
        Verify(Result.Ok("Initial Ok Result")
            .Bind(async value => await Task.FromResult(Result.Ok($"Async Bound: {value}"))), Settings);

    [Fact]
    public Task ShouldHandleAsyncBindingFromSuccessToFailure() =>
        Verify(Result.Ok("Initial Ok Result")
            .Bind(async _ => await Task.FromResult(Result.Fail<string>("Async Failure"))), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    public Task ShouldHandleValueTaskBindingFromSuccessToSuccess() =>
        Verify(Result.Ok("Initial Ok Result")
            .Bind(async value => await new ValueTask<Result<string>>(Result.Ok($"ValueTask Bound: {value}"))), Settings);

    [Fact]
    public Task ShouldHandleValueTaskBindingFromSuccessToFailure() =>
        Verify(Result.Ok("Initial Ok Result")
            .Bind(async _ => await new ValueTask<Result<string>>(
                Result.Fail<string>("ValueTask Failure"))), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    public Task ShouldFailWhenBindingFunctionIsNull() =>
        Throws(() => Result.Ok("Initial Ok Result").Bind((Func<string, Result<string>>)null!), Settings)
        .IgnoreStackTrace();

    [Fact]
    public Task ShouldFailWhenAsyncBindingFunctionIsNull() =>
        Verify(Assert.ThrowsAsync<ArgumentNullException>(() =>
            Result.Ok("Initial Ok Result").Bind((Func<string, Task<Result<string>>>)null!)), Settings);

    [Fact]
    public Task ShouldFailWhenValueTaskBindingFunctionIsNull() =>
        Verify(Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await Result.Ok("Initial Ok Result").Bind((Func<string, ValueTask<Result<string>>>)null!)), Settings);
}
