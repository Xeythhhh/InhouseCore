using System.Diagnostics.CodeAnalysis;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.UnitTests.Primitives.Results.Methods;
public class ResultFailTests : VerifyBaseTest
{
    [Fact]
    public Task ShouldCreateFailedResultWithSingleError() =>
        Verify(Result.Fail(new Error("Test error")), Settings);

    [Fact]
    public Task ShouldCreateFailedResultWithSingleErrorMessage() =>
        Verify(Result.Fail("Test error message"), Settings);

    [Fact]
    [SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "<Pending>")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
    public Task ShouldCreateFailedResultWithMultipleErrorMessages() =>
        Verify(Result.Fail(new[] { "Error message 1", "Error message 2" }), Settings);

    [Fact]
    public Task ShouldCreateFailedResultWithMultipleErrors() =>
        Verify(Result.Fail(new[]
        {
            new Error("Error 1"),
            new Error("Error 2")
        }), Settings);

    [Fact]
    public Task ShouldCreateFailedGenericResultWithSingleError() =>
        Verify(Result.Fail<string>(new Error("Test error")), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    public Task ShouldCreateFailedGenericResultWithSingleErrorMessage() =>
        Verify(Result.Fail<string>("Test error message"), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    [SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "<Pending>")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
    public Task ShouldCreateFailedGenericResultWithMultipleErrorMessages() =>
        Verify(Result.Fail<string>(new[] { "Error message 1", "Error message 2" }), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    public Task ShouldCreateFailedGenericResultWithMultipleErrors() =>
        Verify(Result.Fail<string>(new[]
            {
                new Error("Error 1"),
                new Error("Error 2")
            }), Settings)
            .IgnoreMember<Result<string>>(r => r.Value);

    [Fact]
    public void ShouldThrowArgumentNullExceptionWhenErrorMessagesIsNull() =>
        Throws(() => Result.Fail((IEnumerable<string>)null!));

    [Fact]
    public void ShouldThrowArgumentNullExceptionWhenErrorsIsNull() =>
        Throws(() => Result.Fail((IEnumerable<IError>)null!));

    [Fact]
    public void ShouldThrowArgumentNullExceptionWhenGenericErrorMessagesIsNull() =>
        Throws(() => Result.Fail<string>((IEnumerable<string>)null!));

    [Fact]
    public void ShouldThrowArgumentNullExceptionWhenGenericErrorsIsNull() =>
        Throws(() => Result.Fail<string>((IEnumerable<IError>)null!));
}
