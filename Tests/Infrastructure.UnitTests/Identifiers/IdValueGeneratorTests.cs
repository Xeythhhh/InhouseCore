using Infrastructure.Identifiers;
using Domain.Champions;
using FluentAssertions;

namespace Infrastructure.UnitTests.Identifiers;

public class IdValueGeneratorTests
{
    [Fact]
    public void Next_ReturnsNextId()
    {
        // Arrange
        IdValueGenerator<ChampionId>.SetGeneratorId(420);
        IdValueGenerator<ChampionId> generator = new();

        // Act
        ChampionId result1 = generator.Next(null!);
        ChampionId result2 = generator.Next(null!);

        // Assert
        (result1 < result2).Should().BeTrue();
    }

    [Fact]
    public void Next_Static_ReturnsNextId()
    {
        // Arrange
        IdValueGenerator<ChampionId>.SetGeneratorId(420);

        // Act
        ChampionId result1 = IdValueGenerator<ChampionId>.Next();
        ChampionId result2 = IdValueGenerator<ChampionId>.Next();

        // Assert
        (result1 < result2).Should().BeTrue();
    }
}