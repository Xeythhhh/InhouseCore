using Domain.Primitives;

using FluentAssertions;

using NetArchTest.Rules;

using Xunit.Abstractions;

namespace Tests.ArchitectureTests;
public class DomainTests(ITestOutputHelper output) :
    ArchitectureBaseTest
{
    [Fact]
    public void Entities_ShouldBe_Public()
    {
        // Arrange
        PredicateList types = GetEntityTypes();

        // Act
        TestResult testResult = types
            .Should().BePublic()
            .GetResult();

        // Assert
        OutputTestResults(output, testResult);
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void EntityIds_ShouldBe_PublicAndSealed()
    {
        // Arrange
        PredicateList types = GetEntityIdTypes();

        // Act
        TestResult testResult = types
            .Should().BePublic()
            .And().BeSealed()
            .GetResult();

        // Assert
        OutputTestResults(output, testResult);
        testResult.IsSuccessful.Should().BeTrue();
    }

    private static PredicateList GetEntityTypes() => Types.InAssembly(Domain)
        .That().ResideInNamespace($"{Domain.GetName().Name}.Entities")
        .And().Inherit<EntityBase<IEntityId>>()
        .And().AreNotAbstract()
        .Or().ImplementInterface<IEntity<IEntityId>>()
        .And().AreNotAbstract()
        .And().AreNotInterfaces()
        .And().AreNotStatic();

    private static PredicateList GetEntityIdTypes() => Types.InAssembly(Domain)
        .That().ResideInNamespace($"{Domain.GetName().Name}.Entities")
        .And().Inherit<EntityId<IEntity<IEntityId>>>()
        .And().AreNotAbstract()
        .Or().ImplementInterface<IEntityId>()
        .And().AreNotAbstract()
        .And().AreNotInterfaces()
        .And().AreNotStatic();
}
