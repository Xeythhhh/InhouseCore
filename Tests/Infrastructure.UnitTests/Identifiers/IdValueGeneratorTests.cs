//using Infrastructure.Identifiers;
//using Domain.Champions;
//using FluentAssertions;

//namespace Infrastructure.UnitTests.Identifiers;

//public class IdValueGeneratorTests
//{
//    [Fact]
//    public void Next_ReturnsNextId()
//    {
//        // Arrange
//        IdValueGenerator<Champion.ChampionId>.SetGeneratorId(420);
//        IdValueGenerator<Champion.ChampionId> generator = new();

//        // Act
//        Champion.ChampionId result1 = generator.Next(null!);
//        Champion.ChampionId result2 = generator.Next(null!);

//        // Assert
//        (result1 < result2).Should().BeTrue();
//    }

//    [Fact]
//    public void Next_Static_ReturnsNextId()
//    {
//        // Arrange
//        IdValueGenerator<Champion.ChampionId>.SetGeneratorId(420);

//        // Act
//        Champion.ChampionId result1 = IdValueGenerator<Champion.ChampionId>.Next();
//        Champion.ChampionId result2 = IdValueGenerator<Champion.ChampionId>.Next();

//        // Assert
//        (result1 < result2).Should().BeTrue();
//    }
//}