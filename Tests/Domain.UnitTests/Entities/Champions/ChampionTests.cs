using Domain.Champions;

using FluentAssertions;

using FluentResults;

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
        result.IsFailed.Should().BeTrue();
        result.HasError(e => e.Message.Contains("'Name' must not be empty."))
            .Should().BeTrue();
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
        result.IsFailed.Should().BeTrue();
        result.HasError(e => e.Message.Contains("Name must be less than 100 characters."))
            .Should().BeTrue();
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
        result.IsFailed.Should().BeTrue();
        result.HasError(e => e.Message.Contains("Class must be a valid enum value."))
            .Should().BeTrue();
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
        result.IsFailed.Should().BeTrue();
        result.HasError(e => e.Message.Contains("Role must be a valid enum value."))
            .Should().BeTrue();
    }
}
