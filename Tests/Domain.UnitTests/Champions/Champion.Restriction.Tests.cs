using Domain.Champions;

using FluentAssertions;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.UnitTests.Champions
{
    public class RestrictionTests
    {
        [Fact]
        public void Create_ValidInputsAndHexColorCode_ShouldReturnSuccess()
        {
            // Arrange
            const string name = "Fireball";
            const string identifier = "q";
            const string color = "#FF5733";
            const string reason = "High damage ability";

            // Act
            Result<Champion.Restriction> result = Champion.Restriction.Create(name, identifier, color, reason);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.AbilityName.Should().Be(name);
            result.Value.Identifier.Value.Should().Be(identifier);
            result.Value.ColorHex.Value.Should().Be(color);
            result.Value.Reason.Should().Be(reason);
        }

        [Fact]
        public void Create_ValidInputsAndKnownColorName_ShouldReturnSuccess()
        {
            // Arrange
            const string name = "Fireball";
            const string identifier = "q";
            const string color = "red"; // Known color name resolves to #FF0000
            const string reason = "High damage ability";

            // Act
            Result<Champion.Restriction> result = Champion.Restriction.Create(name, identifier, color, reason);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.AbilityName.Should().Be(name);
            result.Value.Identifier.Value.Should().Be(identifier);
            result.Value.ColorHex.Value.Should().Be("#FF0000");
            result.Value.Reason.Should().Be(reason);
        }

        [Fact]
        public void Create_InvalidColor_ShouldReturnFailure()
        {
            // Arrange
            const string name = "Fireball";
            const string identifier = "q";
            const string invalidColor = "not-a-color";
            const string reason = "High damage ability";

            // Act
            Result<Champion.Restriction> result = Champion.Restriction.Create(name, identifier, invalidColor, reason);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.HasError<Champion.Restriction.CreateChampionRestrictionError>().Should().BeTrue();
            result.Errors[0].Reasons.Should().ContainSingle()
                .Which.Message.Should().Be("Invalid color name / hex format (Parameter 'value')");
        }

        [Fact]
        public void RestrictionColor_Create_ValidHex_ShouldReturnSuccess()
        {
            // Arrange
            const string validHex = "#FF5733";

            // Act
            Result<Champion.Restriction.RestrictionColor> result = Champion.Restriction.RestrictionColor.Create(validHex);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Value.Should().Be(validHex);
        }

        [Fact]
        public void RestrictionColor_Create_InvalidHex_ShouldReturnFailure()
        {
            // Arrange
            const string invalidHex = "invalid-color";

            // Act
            Result<Champion.Restriction.RestrictionColor> result = Champion.Restriction.RestrictionColor.Create(invalidHex);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle()
                .Which.Should().BeOfType<ExceptionalError>()
                .Which.Message.Should().Be("Invalid color name / hex format (Parameter 'value')");
        }

        [Fact]
        public void RestrictionIdentifier_Create_ValidIdentifier_ShouldReturnSuccess()
        {
            // Arrange
            const string validIdentifier = "q";

            // Act
            Result<Champion.Restriction.RestrictionIdentifier> result = Champion.Restriction.RestrictionIdentifier.Create(validIdentifier);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Value.Should().Be(validIdentifier);
        }

        [Fact]
        public void RestrictionIdentifier_Create_InvalidIdentifier_ShouldReturnFailure()
        {
            // Arrange
            const string invalidIdentifier = "x";

            // Act
            Result<Champion.Restriction.RestrictionIdentifier> result = Champion.Restriction.RestrictionIdentifier.Create(invalidIdentifier);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.HasError<Champion.Restriction.RestrictionIdentifier.ValueOutOfRangeError>().Should().BeTrue();
        }

        [Fact]
        public void AddRestriction_ValidInputs_ShouldReturnSuccessAndAddRestriction()
        {
            // Arrange
            Champion champion = Champion.Create("Ezreal", "dps").Value;
            const string abilityName = "Mystic Shot";
            const string identifier = "q";
            const string color = "#FF5733";
            const string reason = "High range poke";

            // Act
            Result<Champion> result = champion.AddRestriction(abilityName, identifier, color, reason);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Restrictions.Should().ContainSingle(); // One restriction should be added
            result.Value.Restrictions[0].AbilityName.Should().Be(abilityName);
            result.Value.Restrictions[0].Identifier.Value.Should().Be(identifier);
            result.Value.Restrictions[0].ColorHex.Value.Should().Be(color);
            result.Value.Restrictions[0].Reason.Should().Be(reason);
            result.Value.HasRestrictions.Should().BeTrue(); // Should set HasRestrictions to true
        }

        [Fact]
        public void AddRestriction_InvalidColor_ShouldReturnFailure()
        {
            // Arrange
            Champion champion = Champion.Create("Ezreal", "dps").Value;
            const string abilityName = "Mystic Shot";
            const string identifier = "q";
            const string invalidColor = "invalidColor"; // Invalid color format
            const string reason = "High range poke";

            // Act
            Result<Champion> result = champion.AddRestriction(abilityName, identifier, invalidColor, reason);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.HasError<Champion.Restriction.CreateChampionRestrictionError>().Should().BeTrue();
            result.Errors[0].Reasons.Should().ContainSingle()
                .Which.Message.Should().Be("Invalid color name / hex format (Parameter 'value')");
        }

        [Fact]
        public void AddRestriction_InvalidIdentifier_ShouldReturnFailure()
        {
            // Arrange
            Champion champion = Champion.Create("Ezreal", "dps").Value;
            const string abilityName = "Mystic Shot";
            const string invalidIdentifier = "invalid"; // Invalid identifier
            const string color = "#FF5733";
            const string reason = "High range poke";

            // Act
            Result<Champion> result = champion.AddRestriction(abilityName, invalidIdentifier, color, reason);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.HasError<Champion.Restriction.CreateChampionRestrictionError>().Should().BeTrue();
            result.Errors[0].Reasons.Should().ContainSingle()
                .Which.Should().BeOfType<ExceptionalError>()
                .Which.Message.Should().Be(Champion.Restriction.RestrictionIdentifier.ValueOutOfRangeError.MessageTemplate);
        }
    }
}
