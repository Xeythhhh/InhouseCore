using SharedKernel.Primitives.Reasons;

namespace SharedKernel.UnitTests.Primitives.Reasons;

public class SuccessTests : VerifyBaseTest
{
    [Fact]
    public async Task ShouldCreateSuccessWithMessage() =>
        await Verify(new Success("Test message"), Settings);

    [Fact]
    public async Task ShouldAddMetadata() =>
        await Verify(new Success("Test success")
            .WithMetadata("Key", "Value"), Settings);

    [Fact]
    public async Task ShouldAddMultipleMetadata() =>
        await Verify(new Success("Test success")
            .WithMetadata(new Dictionary<string, object>
            {
                { "Key1", "Value1" },
                { "Key2", "Value2" }
            }), Settings);

    [Fact]
    public async Task ShouldHandleEmptyMetadataGracefully() =>
        await Verify(new Success("Test success"), Settings);

    [Fact]
    public async Task ShouldReturnStringRepresentation() =>
        await Verify(new Success("Test success")
            .WithMetadata("Key", "Value").ToString(), Settings);

    [Fact]
    public async Task ShouldCreateSuccessUsingDefaultFactory() =>
        await Verify(Success.DefaultFactory("Factory success message"), Settings);
}
