using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.UnitTests.Primitives.Results.Methods;

public class ResultBindTests : VerifyBaseTest
{
    [Fact]
    public Task ShouldBindSuccessToAnotherSuccess() =>
        Verify(Result.Ok()
            .Bind(() => Result.Ok("Bound Ok Result")), Settings);

    [Fact]
    public Task ShouldBindFailureToAnotherResult() =>
        Verify(Result.Fail("Initial Failure Result")
                .Bind(() => Result.Ok("Shouldn't Bind")), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    public Task ShouldBindSuccessToFailure() =>
        Verify(Result.Ok()
                .Bind(() => Result.Fail<string>("Binding Failure Error")), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    public Task ShouldPropagateReasonsWhenBindingSuccessToSuccess() =>
        Verify(Result.Ok()
            .WithSuccess("Initial Success")
            .Bind(() => Result.Ok("Bound Ok Result").WithSuccess("Another Success")), Settings);

    [Fact]
    public Task ShouldHandleAsyncBindingFromSuccessToSuccess() =>
        Verify(Result.Ok()
            .Bind(async () => await Task.FromResult(Result.Ok("Async Ok Result"))), Settings);

    [Fact]
    public Task ShouldHandleAsyncBindingFromSuccessToFailure() =>
        Verify(Result.Ok()
                .Bind(async () => await Task.FromResult(Result.Fail<string>("Async Failure Result"))), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    public Task ShouldHandleValueTaskBindingFromSuccessToSuccess() =>
        Verify(Result.Ok()
            .Bind(async () => await new ValueTask<Result<string>>(Result.Ok("ValueTask Ok Result"))), Settings);

    [Fact]
    public Task ShouldHandleValueTaskBindingFromSuccessToFailure() =>
        Verify(Result.Ok()
                .Bind(async () => await new ValueTask<Result<string>>(
                    Result.Fail<string>("ValueTask Failure Result"))), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    public Task ShouldFailWhenBindingFunctionIsNull() =>
        Throws(() => Result.Ok().Bind((Func<Result<string>>)null!), Settings)
            .IgnoreStackTrace();

    [Fact]
    public Task ShouldFailWhenAsyncBindingFunctionIsNull() =>
         Verify(Assert.ThrowsAsync<ArgumentNullException>(() =>
            Result.Ok().Bind((Func<Task<Result<string>>>)null!)), Settings);

    [Fact]
    public Task ShouldFailWhenValueTaskBindingFunctionIsNull() =>
        Verify(Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await Result.Ok().Bind((Func<ValueTask<Result<string>>>)null!)), Settings);
}
