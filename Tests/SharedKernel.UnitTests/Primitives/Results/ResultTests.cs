using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace SharedKernel.UnitTests.Primitives.Results;

public class ResultTests : VerifyBaseTest
{
    [Fact]
    public Task ShouldConvertSingleErrorToResult() =>
        Verify((Result)new Error("Test error"),
            Settings);

    [Fact]
    public Task ShouldConvertErrorListToResult() =>
        Verify((Result)new List<Error>()
                {
                    new("Error 1"),
                    new("Error 2")
                },
            Settings);
}
