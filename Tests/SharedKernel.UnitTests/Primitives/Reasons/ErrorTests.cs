using SharedKernel.Primitives.Reasons;

namespace SharedKernel.UnitTests.Primitives.Reasons;

public class ErrorTests : VerifyBaseTest
{
    [Fact]
    public Task ShouldCreateErrorWithMessage() =>
        Verify(new Error("Test message"), Settings);

    [Fact]
    public Task ShouldAddSingleCause() =>
        Verify(new Error("Test error")
            .CausedBy("Cause 1"), Settings);

    [Fact]
    public Task ShouldAddSingleCauseFromConstructor() =>
        Verify(new Error("Test error", new Error("Cause 1")), Settings);

    [Fact]
    public Task ShouldAddMultipleCauses() =>
        Verify(new Error("Test error")
            .CausedBy("Cause 1")
            .CausedBy("Cause 2"), Settings);

    [Fact]
    public Task ShouldAddMultipleCausesStringEnumerable() =>
        Verify(new Error("Test error")
            .CausedBy(["Cause 1", "Cause 2"]), Settings);

    [Fact]
    public Task ShouldAddMultipleCausesStringParams() =>
        Verify(new Error("Test error")
            .CausedBy("Cause 1", "Cause 2"), Settings);

    [Fact]
    public Task ShouldAddMultipleCausesIErrorParams() =>
        Verify(new Error("Test error")
            .CausedBy(new Error("Error 1"), new Error("Error 2")), Settings);

    [Fact]
    public Task ShouldAddMultipleCausesIErrorEnumerable() =>
        Verify(new Error("Test error")
            .CausedBy([(new Error("Error 1")), (new Error("Error 2"))]), Settings);

    [Fact]
    public Task ShouldAddExceptionAsCause() =>
        Verify(new Error("Test error")
            .CausedBy(new Exception("Test exception")), Settings);

    [Fact]
    public Task ShouldAddErrorAsCause() =>
        Verify(new Error("Test error")
            .CausedBy(new Error("Cause error")), Settings);

    [Fact]
    public Task ShouldAddMultipleNestedCauses() =>
        Verify(new Error("Outer error")
            .CausedBy(new Error("Inner error")
                .CausedBy("Deeper cause"))
            .CausedBy(new Exception("Another exception")), Settings);

    [Fact]
    public Task ShouldAddMetadata() =>
        Verify(new Error("Test error")
            .WithMetadata("Key", "Value"), Settings);

    [Fact]
    public Task ShouldAddMultipleMetadata() =>
        Verify(new Error("Test error")
            .WithMetadata(new Dictionary<string, object>
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            }), Settings);

    [Fact]
    public Task ShouldReturnStringRepresentation() =>
        Verify(new Error("Test error")
            .WithMetadata("Key", "Value")
            .CausedBy("Cause 1")
            .CausedBy(new Exception("Test exception")).ToString(), Settings);
}
