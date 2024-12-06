using SharedKernel.Primitives.Result;

namespace SharedKernel.UnitTests.Primitives.Results.Methods;

public class ResultOkTests : VerifyBaseTest
{
    [Fact]
    public Task ShouldCreateNonGenericSuccessResult() =>
        Verify(Result.Ok(), Settings);

    [Fact]
    public Task ShouldCreateGenericSuccessResultWithValue() =>
        Verify(Result.Ok(420), Settings);
}
