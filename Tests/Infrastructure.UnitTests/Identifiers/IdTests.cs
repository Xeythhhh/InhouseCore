using System.Reflection;

using Domain.Champions;
using Domain.Primitives;

using FluentAssertions;

using FluentResults;

using IdGen;

using Infrastructure.Exceptions;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Id = Infrastructure.Identifiers.Id;

namespace Infrastructure.UnitTests.Identifiers;

public class IdTests
{
    [Fact]
    public void RegisterGeneratorId_ShouldSet_GeneratorId()
    {
        // Arrange
        const int generatorId = 420;

        // Act
        Id.RegisterGeneratorId(generatorId);

        // Assert
        Type? valueGeneratorType = Id.GetValueGenerator<ChampionId>();
        valueGeneratorType.Should().NotBeNull();

        IdGenerator? generator = (IdGenerator?)valueGeneratorType?
            .GetField("_generator", BindingFlags.Static | BindingFlags.NonPublic)?
            .GetValue(null);

        generator.Should().NotBeNull();
        generator?.Id.Should().Be(420);
    }

    [Fact]
    public void GetValueConverter_ShouldReturnConverter_WhenConverterRegistered()
    {
        // Arrange
        Id.RegisterConverters();

        // Act
        ValueConverter converter = Id.GetValueConverter<ChampionId>();

        // Assert
        converter.Should().NotBeNull();
    }

    [Fact]
    public void GetValueConverter_ShouldThrow_WhenConverterNotRegistered()
    {
        // Act
        Action act = () => Id.GetValueConverter<UnregisteredId>();

        // Assert
        act.Should().Throw<ValueConverterNotRegisteredException>()
            .WithMessage($"No ValueConverter registered for type '{typeof(UnregisteredId).AssemblyQualifiedName}'");
    }

    [Fact]
    public void RegisterConverters_ShouldReturnOk_WhenNoErrors()
    {
        // Act
        Result result = Id.RegisterConverters();

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    private sealed record UnregisteredId : IEntityId
    {
        public long Value { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
    }
}
