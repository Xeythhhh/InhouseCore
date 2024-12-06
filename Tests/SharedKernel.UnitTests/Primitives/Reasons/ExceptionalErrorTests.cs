using SharedKernel.Primitives.Reasons;

namespace SharedKernel.UnitTests.Primitives.Reasons;

public class ExceptionalErrorTests : VerifyBaseTest
{
    [Fact]
    public async Task ShouldCreateExceptionalErrorWithMessageAndException() =>
        await Verify(new ExceptionalError("Custom error message", new Exception("Test exception")), Settings);

    [Fact]
    public async Task ShouldCreateExceptionalErrorFromExceptionOnly() =>
        await Verify(new ExceptionalError(new Exception("Test exception")), Settings);

    [Fact]
    public async Task ShouldHandleEmptyMetadataAndReasonsGracefully() =>
        await Verify(new ExceptionalError("Error with no metadata or reasons", new Exception("Test exception")), Settings);

    [Fact]
    public async Task ShouldHandleComplexNestedCauses() =>
        await Verify(new ExceptionalError("Outer error", new Exception("Outer exception"))
            .CausedBy<ExceptionalError>(new ExceptionalError("Inner error", new Exception("Inner exception"))
                .CausedBy<ExceptionalError>(new Exception("Innermost exception"))), Settings);
}
