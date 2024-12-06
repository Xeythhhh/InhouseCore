using MediatR;

using NetArchTest.Rules;

using Xunit.Abstractions;

namespace Tests.ArchitectureTests;
public class ApplicationTests(ITestOutputHelper output) :
    ArchitectureBaseTest
{
    [Fact]
    public async Task Application_ShouldOnlyDefine_CommandsQueriesAndHandlers()
    {
        // Arrange
        PredicateList types = Types.InAssembly(Application)
            .That().ResideInNamespace(Application.GetName().Name)
            .And().ArePublic()
            .And().AreNotAbstract()
            .And().AreNotInterfaces()
            .And().AreNotStatic();

        // Act
        TestResult testResult = types
            .Should().HaveNameEndingWith("Handler")
            .Or().HaveNameEndingWith("Command")
            .Or().HaveNameEndingWith("Query")
            .And().HaveDependencyOnAll("MediatR")
            .GetResult();

        // Assert
        OutputTestResults(output, testResult);
        await Verify(testResult.IsSuccessful, Settings);
    }

    [Fact]
    public async Task Handlers_Should_BeSealed()
    {
        // Arrange
        PredicateList types = Types.InAssembly(Application)
            .That().ImplementInterface(typeof(IRequestHandler<,>))
            .And().AreNotAbstract();

        // Act
        TestResult testResult = types
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        OutputTestResults(output, testResult);
        await Verify(testResult.IsSuccessful, Settings);
    }
}
