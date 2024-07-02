using Domain.Champions.ValueObjects;
using Domain.Errors;

using FluentAssertions;

using SharedKernel.Primitives.Result;

namespace Domain.UnitTests.Champions;
public class ChampionRoleTests
{
    [Fact]
    public void Create_WithValidValue_ReturnsSuccessResult()
    {
        // Arrange
        const string validValue = "dps";

        // Act
        Result<ChampionRole> result = ChampionRole.Create(validValue);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(validValue);
    }

    [Fact]
    public void Create_WithInvalidValue_ReturnsFailureResult()
    {
        // Arrange
        const string invalidValue = "Invalid";

        // Act
        Result<ChampionRole> result = ChampionRole.Create(invalidValue);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainItemsAssignableTo<ChampionRole.ValueOutOfRangeError>();
    }

    [Fact]
    public void ImplicitConversion_WithValidValue_ReturnsChampionRole()
    {
        // Arrange
        const string validValue = "support";

        // Act
        ChampionRole role = validValue;

        // Assert
        role.Value.Should().Be(validValue);
    }

    [Fact]
    public void ImplicitConversion_WithInvalidValue_ThrowsInvalidOperationException()
    {
        // Arrange
        const string invalidValue = "Invalid";

        // Act
        Action act = () => { ChampionRole role = invalidValue; };

        // Assert
        act.Should().Throw<Exception>()
            .WithMessage(DomainErrors.InvalidValueForImplicitConversionError.ErrorMessageTemplate);
    }

    [Fact]
    public void ExplicitConversion_ReturnsStringValue()
    {
        // Arrange
        ChampionRole role = ChampionRole.Create("Dps").Value;

        // Act
        string result = (string)role;

        // Assert
        result.Should().Be(role.Value);
    }
}
