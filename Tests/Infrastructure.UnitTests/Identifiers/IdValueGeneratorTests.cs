using Domain.Primitives.Result;

using FluentAssertions;

using Infrastructure.Identifiers;

using Xunit.Abstractions;

namespace Infrastructure.UnitTests.Identifiers;

public class IdValueGeneratorTestFixture
{
    public readonly int TestId = 420;
}

public class IdValueGeneratorTests(IdValueGeneratorTestFixture fixture, ITestOutputHelper output) :
    IClassFixture<IdValueGeneratorTestFixture>
{
    [Fact]
    public void Register_FailureWhenAlreadyRegistered()
    {
        Result result = IdValueGenerator.Register(fixture.TestId); // Register once
        result.IsSuccess.Should().BeTrue();

        result = IdValueGenerator.Register(fixture.TestId); // Try to register again

        if (result.IsFailure) output.WriteLine(result.Error);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("IdValueGenerator already registered");
    }
}
