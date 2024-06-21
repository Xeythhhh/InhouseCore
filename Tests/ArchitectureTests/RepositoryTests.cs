using Domain.Abstractions;

using FluentAssertions;

using NetArchTest.Rules;

using Xunit.Abstractions;

namespace Tests.ArchitectureTests;

public class RepositoryTests(ITestOutputHelper output) :
    ArchitectureBaseTest
{
    [Fact]
    public void RepositoryInterfaces_ShouldImplement_IRepository()
    {
        // Arrange
        PredicateList types = Types.InAssembly(Domain)
            .That().HaveNameEndingWith("Repository")
            .And().AreNotOfType(typeof(IRepository), typeof(IRepository<,>));

        // Act
        TestResult testResult = types.Should().ImplementInterface(typeof(IRepository<,>)).GetResult();

        // Assert
        OutputTestResults(output, testResult);
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void RepositoryImplementations_ShouldImplement_IRepository()
    {
        // Arrange

        PredicateList types = Types.InAssembly(Infrastructure)
            .That().HaveNameEndingWith("Repository");

        // Act
        TestResult testResult = types.Should().ImplementInterface(typeof(IRepository<,>)).GetResult();

        // Assert
        OutputTestResults(output, testResult);
        testResult.IsSuccessful.Should().BeTrue();
    }
}
