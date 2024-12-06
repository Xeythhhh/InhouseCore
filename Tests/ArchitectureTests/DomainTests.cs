using Domain.Primitives;

using NetArchTest.Rules;

using Xunit.Abstractions;

namespace Tests.ArchitectureTests;
public class DomainTests(ITestOutputHelper output) :
    ArchitectureBaseTest
{
    [Fact]
    public async Task Entities_ShouldBe_Public()
    {
        // Arrange
        PredicateList types = GetEntityTypes();

        // Act
        TestResult testResult = types
            .Should().BePublic()
            .GetResult();

        // Assert
        OutputTestResults(output, testResult);
        await Verify(testResult.IsSuccessful, Settings);
    }

    [Fact]
    public async Task EntityIds_ShouldBe_PublicAndSealed()
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
        await Verify(testResult.IsSuccessful, Settings);
    }

    private static PredicateList GetEntityTypes() => Types.InAssembly(Domain)
        .That().Inherit(typeof(EntityBase<>)).And().AreNotAbstract()
        .Or().ImplementInterface<IEntity>().And().AreNotAbstract();

    private static PredicateList GetEntityIdTypes() => Types.InAssembly(Domain)
        .That().Inherit(typeof(EntityId<>)).And().AreNotAbstract()
        .Or().ImplementInterface<IEntityId>().And().AreNotAbstract();
}
