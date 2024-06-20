using System.Reflection;
using Domain.Champions;
using Domain.UnitTests.TestImplementations;
using Domain.Users;
using FluentAssertions;

using Infrastructure.Exceptions;
using Infrastructure.Identifiers;

namespace Infrastructure.UnitTests.Identifiers;

public class IdTestFixture
{
    public readonly int TestId = 420;
}

public class IdTests(IdTestFixture fixture) :
    IClassFixture<IdTestFixture>
{
    [Fact]
    public void GetValueConverter_ShouldThrowExceptionWhenNotRegistered() =>
        Assert.Throws<ValueConverterNotRegisteredException>(Id.GetValueConverter<TestEntity>);

    [Fact]
    public void RegisterConverters_ShouldRegisterValueConverters()
    {
        // Act
        Id.RegisterConverters();

        // Assert
        Id.GetValueConverter<ApplicationUser>().Should().NotBeNull();
        Id.GetValueConverter<ApplicationRole>().Should().NotBeNull();
        Id.GetValueConverter<Champion>().Should().NotBeNull();
    }

    [Fact]
    public void RegisterGenerator_SuccessfulRegistration()
    {
        // Arrange
        ResetStaticState();

        // Act
        Action action = () => Id.RegisterGenerator(fixture.TestId);

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void RegisterGenerator_FailureOnDuplicateRegistration()
    {
        // Arrange
        ResetStaticState();
        Id.RegisterGenerator(fixture.TestId); // Register a generator

        // Act 
        Action action = () => Id.RegisterGenerator(fixture.TestId); // Try to register again

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }

    private static void ResetStaticState() =>
        typeof(IdValueGenerator)
            .GetField("_generator", BindingFlags.Static | BindingFlags.NonPublic)?
            .SetValue(null, null);
}
