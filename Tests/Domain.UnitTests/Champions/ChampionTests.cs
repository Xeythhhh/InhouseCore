using Domain.Champions;
using Domain.Primitives;

using FluentAssertions;

using FluentResults;

namespace Domain.UnitTests.Champions
{
    public class ChampionTests
    {
        [Fact]
        public void Create_ShouldReturnSuccessResult_WhenValidParametersAreProvided()
        {
            // Arrange
            const string name = "ChampionName";
            const string role = "melee";

            // Act
            Result<Champion> result = Champion.Create(name, role);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Value.Should().Be(name);
            result.Value.Role.Value.Should().Be(role);
        }

        [Fact]
        public void Create_ShouldReturnFailureResult_WhenExceptionIsThrown()
        {
            // Arrange
            const string? invalidName = null;
            const string role = "InvalidRole";

            // Act
            Result<Champion> result = Champion.Create(invalidName!, role);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().Contain(e => e.Message == Champion.Errors.Create)
                .Which.Reasons.Should().Contain(r => r.Message == ValueObjectCommonErrors.InvalidValueForImplicitConversion);
        }
    }
}
