using Domain.Champions;
using Domain.Primitives.Result;

using FluentAssertions;

namespace Domain.UnitTests.Entities.Champions;

public class ChampionTests
{
    [Fact]
    public void Create_ValidParameters_ShouldReturnSuccess()
    {
        // Arrange
        const string validName = "ChampionName";
        const Champion.Classes validClass = Champion.Classes.Melee;
        const Champion.Roles validRole = Champion.Roles.Dps;

        // Act
        Result<Champion> result = Champion.Create(validName, validClass, validRole);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Name.Should().Be(validName);
        result.Value.Class.Should().Be(validClass);
        result.Value.Role.Should().Be(validRole);
    }

    [Fact]
    public void Create_EmptyName_ShouldReturnFailure()
    {
        // Arrange
        string invalidName = string.Empty;
        const Champion.Classes validClass = Champion.Classes.Melee;
        const Champion.Roles validRole = Champion.Roles.Dps;

        // Act
        Result<Champion> result = Champion.Create(invalidName, validClass, validRole);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain("'Name' must not be empty.");
    }

    [Fact]
    public void Create_NameExceedsMaxLength_ShouldReturnFailure()
    {
        // Arrange
        string invalidName = new('a', 101); // Name with 101 characters
        const Champion.Classes validClass = Champion.Classes.Melee;
        const Champion.Roles validRole = Champion.Roles.Dps;

        // Act
        Result<Champion> result = Champion.Create(invalidName, validClass, validRole);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain("Name must be less than 100 characters.");
    }

    [Fact]
    public void Create_InvalidClass_ShouldReturnFailure()
    {
        // Arrange
        const string validName = "ChampionName";
        const Champion.Classes invalidClass = (Champion.Classes)999; // Invalid enum value
        const Champion.Roles validRole = Champion.Roles.Dps;

        // Act
        Result<Champion> result = Champion.Create(validName, invalidClass, validRole);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain("Class must be a valid enum value.");
    }

    [Fact]
    public void Create_InvalidRole_ShouldReturnFailure()
    {
        // Arrange
        const string validName = "ChampionName";
        const Champion.Classes validClass = Champion.Classes.Melee;
        const Champion.Roles invalidRole = (Champion.Roles)999; // Invalid enum value

        // Act
        Result<Champion> result = Champion.Create(validName, validClass, invalidRole);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain("Role must be a valid enum value.");
    }
}
