﻿using Domain.Champions;

using FluentAssertions;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.UnitTests.Champions
{
    public class AugmentTests
    {
        [Fact]
        public void Create_ValidInputsAndHexColorCode_ShouldReturnSuccess()
        {
            // Arrange
            const string name = "Fireball";
            const string target = "q";
            const string color = "#FF5733";

            // Act
            Result<Champion.Augment> result = Champion.Augment.Create(name, target, color);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Should().Be(name);
            result.Value.Target.Value.Should().Be(target);
            result.Value.ColorHex.Value.Should().Be(color);
        }

        [Fact]
        public void Create_ValidInputsAndKnownColorName_ShouldReturnSuccess()
        {
            // Arrange
            const string name = "Fireball";
            const string target = "q";
            const string color = "red"; // Known color name resolves to #FF0000

            // Act
            Result<Champion.Augment> result = Champion.Augment.Create(name, target, color);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Should().Be(name);
            result.Value.Target.Value.Should().Be(target);
            result.Value.ColorHex.Value.Should().Be("#FF0000");
        }

        [Fact]
        public void Create_InvalidColor_ShouldReturnFailure()
        {
            // Arrange
            const string name = "Fireball";
            const string target = "q";
            const string invalidColor = "not-a-color";

            // Act
            Result<Champion.Augment> result = Champion.Augment.Create(name, target, invalidColor);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.HasError<Champion.Augment.CreateChampionAugmentError>().Should().BeTrue();
            result.Errors[0].Reasons.Should().ContainSingle()
                .Which.Message.Should().Be("Invalid color name / hex format (Parameter 'value')");
        }

        [Fact]
        public void AugmentColor_Create_ValidHex_ShouldReturnSuccess()
        {
            // Arrange
            const string validHex = "#FF5733";

            // Act
            Result<Champion.Augment.AugmentColor> result = Champion.Augment.AugmentColor.Create(validHex);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Value.Should().Be(validHex);
        }

        [Fact]
        public void AugmentColor_Create_InvalidHex_ShouldReturnFailure()
        {
            // Arrange
            const string invalidHex = "invalid-color";

            // Act
            Result<Champion.Augment.AugmentColor> result = Champion.Augment.AugmentColor.Create(invalidHex);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle()
                .Which.Should().BeOfType<ExceptionalError>()
                .Which.Message.Should().Be("Invalid color name / hex format (Parameter 'value')");
        }

        [Fact]
        public void AugmentTarget_Create_ValidTarget_ShouldReturnSuccess()
        {
            // Arrange
            const string validTarget = "q";

            // Act
            Result<Champion.Augment.AugmentTarget> result = Champion.Augment.AugmentTarget.Create(validTarget);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Value.Should().Be(validTarget);
        }

        [Fact]
        public void AugmentTarget_Create_InvalidTarget_ShouldReturnFailure()
        {
            // Arrange
            const string invalidTarget = "x";

            // Act
            Result<Champion.Augment.AugmentTarget> result = Champion.Augment.AugmentTarget.Create(invalidTarget);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.HasError<Champion.Augment.AugmentTarget.ValueOutOfRangeError>().Should().BeTrue();
        }

        [Fact]
        public void AddAugment_ValidInputs_ShouldReturnSuccessAndAddAugment()
        {
            // Arrange
            Champion champion = Champion.Create("Ezreal", "dps").Value;
            const string augmentName = "Mystic Shot";
            const string target = "q";
            const string color = "#FF5733";

            // Act
            Result<Champion> result = champion.AddAugment(augmentName, target, color);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Augments.Should().ContainSingle(); // One restriction should be added
            result.Value.Augments[0].Name.Should().Be(augmentName);
            result.Value.Augments[0].Target.Value.Should().Be(target);
            result.Value.Augments[0].ColorHex.Value.Should().Be(color);
        }

        [Fact]
        public void AddAugment_InvalidColor_ShouldReturnFailure()
        {
            // Arrange
            Champion champion = Champion.Create("Ezreal", "dps").Value;
            const string augmentName = "Mystic Shot";
            const string target = "q";
            const string invalidColor = "invalidColor"; // Invalid color format

            // Act
            Result<Champion> result = champion.AddAugment(augmentName, target, invalidColor);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.HasError<Champion.Augment.CreateChampionAugmentError>().Should().BeTrue();
            result.Errors[0].Reasons.Should().ContainSingle()
                .Which.Message.Should().Be("Invalid color name / hex format (Parameter 'value')");
        }

        [Fact]
        public void AddAugment_InvalidTarget_ShouldReturnFailure()
        {
            // Arrange
            Champion champion = Champion.Create("Ezreal", "dps").Value;
            const string augmentName = "Mystic Shot";
            const string invalidTarget = "invalid"; // Invalid target
            const string color = "#FF5733";

            // Act
            Result<Champion> result = champion.AddAugment(augmentName, invalidTarget, color);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.HasError<Champion.Augment.CreateChampionAugmentError>().Should().BeTrue();
            result.Errors[0].Reasons.Should().ContainSingle()
                .Which.Should().BeOfType<ExceptionalError>()
                .Which.Message.Should().Be(Champion.Augment.AugmentTarget.ValueOutOfRangeError.MessageTemplate);
        }
    }
}
