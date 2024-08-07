﻿using FluentAssertions;

using MediatR;

using NetArchTest.Rules;

using Xunit.Abstractions;

namespace Tests.ArchitectureTests;
public class ApplicationTests(ITestOutputHelper output) :
    ArchitectureBaseTest
{
    [Fact]
    public void Application_ShouldOnlyDefine_CommandsQueriesAndHandlers()
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
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handlers_Should_BeSealed()
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
        testResult.IsSuccessful.Should().BeTrue();
    }
}
