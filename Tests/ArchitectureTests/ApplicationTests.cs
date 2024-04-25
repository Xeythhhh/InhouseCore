﻿using FluentAssertions;

using NetArchTest.Rules;

using Xunit.Abstractions;

namespace Tests.ArchitectureTests;
public class ApplicationTests(ITestOutputHelper output) : ArchitectureBaseTest
{
    [Fact]
    public void Application_ShouldOnlyDefine_CommandsQuerriesAndHandlers()
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
            .And().HaveDependencyOnAll(
                Domain.GetName().Name,
                "MediatR")
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
        OutputTestResults(output, testResult);
    }

    [Fact]
    public void Handlers_Should_BeSealed()
    {
        throw new NotImplementedException();

        //// Arrange
        //var types = Types.InAssembly(Application)
        //    .That().ImplementInterface(typeof(IRequestHandler<,>));

        //// Act
        //var testResult = types
        //    .Should()
        //    .BeSealed()
        //    .GetResult();

        //// Assert
        //testResult.IsSuccessful.Should().BeTrue();
        //OutputTestResults(output, testResult);
    }
}