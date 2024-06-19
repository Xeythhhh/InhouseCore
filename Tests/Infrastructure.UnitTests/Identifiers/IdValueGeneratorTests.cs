using CSharpFunctionalExtensions;

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
    public void Create_FailureWhenAlreadyInitialized()
    {
        Result result = IdValueGenerator.Create(fixture.TestId); // Initialize once
        result.IsSuccess.Should().BeTrue();

        result = IdValueGenerator.Create(fixture.TestId); // Try to initialize again

        if (result.IsFailure) output.WriteLine(result.Error);
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("IdValueGenerator already registered");
    }
}
