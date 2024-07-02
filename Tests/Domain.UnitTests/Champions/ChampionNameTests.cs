using System.Collections;

using Domain.Champions.ValueObjects;
using Domain.Errors;
using Domain.Primitives;

using FluentAssertions;

using SharedKernel.Primitives.Result;

namespace Domain.UnitTests.Champions;

public class ChampionNameTests
{
    [Fact]
    public void Create_ShouldReturnSuccess_WhenNameIsValid()
    {
        // Arrange
        const string validName = "ValidChampion";

        // Act
        Result<ChampionName> result = ChampionName.Create(validName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Value.Should().Be(validName);
    }

    [Theory]
    [ClassData(typeof(InvalidNames))]
    public void Create_ShouldReturnFailure_WhenNameIsInvalid(string invalidName, Type expectedErrorType)
    {
        // Act
        Result<ChampionName> result = ChampionName.Create(invalidName);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.GetType() == expectedErrorType);
    }

    [Fact]
    public void ImplicitConversion_ShouldReturnChampionName_WhenValidStringProvided()
    {
        // Arrange
        const string validName = "ValidChampion";

        // Act
        ChampionName championName = validName;

        // Assert
        championName.Value.Should().Be(validName);
    }

    [Fact]
    public void ImplicitConversion_ShouldThrowException_WhenInvalidStringProvided()
    {
        // Arrange
        const string invalidName = "";

        // Act
        Action act = () => { ChampionName championName = invalidName; };

        // Assert
        act.Should().Throw<Exception>()
            .WithMessage(DomainErrors.InvalidValueForImplicitConversionError.ErrorMessageTemplate);
    }

    [Fact]
    public void ExplicitConversion_ShouldReturnStringValue_WhenChampionNameProvided()
    {
        // Arrange
        const string validName = "ValidChampion";
        ChampionName championName = validName;

        // Act
        string result = (string)championName;

        // Assert
        result.Should().Be(validName);
    }
}

internal class InvalidNames : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "", typeof(DomainErrors.NullOrEmptyError) };
        yield return new object[] { null!, typeof(DomainErrors.NullOrEmptyError) };
        yield return new object[] { new string('a', 101), typeof(ChampionName.GreaterThan101CharactersError) };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}