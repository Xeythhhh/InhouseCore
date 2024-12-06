//using System.Collections;

//using Domain.Champions;

//using FluentAssertions;

//using SharedKernel.Primitives.Reasons;
//using SharedKernel.Primitives.Result;

//namespace Domain.UnitTests.Champions;
//public class ChampionTests
//{
//    [Fact]
//    public void Create_ShouldReturnSuccessResult_WhenValidParametersAreProvided()
//    {
//        // Arrange
//        const string name = "ChampionName";
//        const string role = "tank";
//        const string portraitUri = "https://example.com/portrait.png";
//        const string wideUri = "https://example.com/wide.png";

//        // Act
//        Result<Champion> result = Champion.Create(name, role, portraitUri, wideUri);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.Value.Name.Value.Should().Be(name);
//        result.Value.Role.Value.Should().Be("Tank"); // the domain logic capitalizes role name
//        result.Value.Avatar.Portrait.Should().Be(portraitUri);
//        result.Value.Avatar.PortraitWide.Should().Be(wideUri);
//    }

//    [Fact]
//    public void Create_ShouldReturnFailureResult_WhenExceptionIsThrown()
//    {
//        // Arrange
//        const string role = "InvalidRole";
//        const string portraitUri = "";
//        const string wideUri = "";

//        // Act
//        Result<Champion> result = Champion.Create(string.Empty, role, portraitUri, wideUri);

//        // Assert
//        result.IsFailed.Should().BeTrue();
//        result.HasError<Champion.CreateChampionError>().Should().BeTrue();
//    }

//    [Fact]
//    public void CreateChampionName_ShouldReturnSuccess_WhenNameIsValid()
//    {
//        // Arrange
//        const string validName = "ValidChampion";

//        // Act
//        Result<Champion.ChampionName> result = Champion.ChampionName.Create(validName);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.Value.Value.Should().Be(validName);
//    }

//    [Theory]
//    [ClassData(typeof(InvalidNames))]
//    public void CreateChampionName_ShouldReturnFailure_WhenNameIsInvalid(string invalidName, Type expectedErrorType, Type? expectedExceptionType)
//    {
//        // Act
//        Result<Champion.ChampionName> result = Champion.ChampionName.Create(invalidName);

//        // Assert
//        result.IsFailed.Should().BeTrue();
//        result.Errors.Should().ContainSingle()
//            .Which.Should().BeOfType(expectedErrorType);

//        if (expectedExceptionType is not null)
//            result.Errors[0].As<ExceptionalError>().Exception.Should().BeOfType(expectedExceptionType);
//    }

//    [Fact]
//    public void ChampionNameImplicitConversion_ShouldReturnChampionName_WhenValidStringProvided()
//    {
//        // Arrange
//        const string validName = "ValidChampion";

//        // Act
//        Champion.ChampionName championName = validName;

//        // Assert
//        championName.Value.Should().Be(validName);
//    }

//    [Fact]
//    public void ChampionNameImplicitConversion_ShouldThrowException_WhenInvalidStringProvided()
//    {
//        // Arrange
//        const string invalidName = "";

//        // Act
//        Action act = () => { Champion.ChampionName championName = invalidName; };

//        // Assert
//        act.Should().Throw<InvalidOperationException>();
//    }

//    [Fact]
//    public void ChampionNameExplicitConversion_ShouldReturnStringValue_WhenChampionNameProvided()
//    {
//        // Arrange
//        const string validName = "ValidChampion";
//        Champion.ChampionName championName = validName;

//        // Act
//        string result = (string)championName;

//        // Assert
//        result.Should().Be(validName);
//    }

//    [Fact]
//    public void CreateChampionRole_WithValidValue_ReturnsSuccessResult()
//    {
//        // Arrange
//        const string validValue = "Dps";

//        // Act
//        Result<Champion.ChampionRole> result = Champion.ChampionRole.Create(validValue);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.Value.Value.Should().Be(validValue);
//    }

//    [Fact]
//    public void CreateChampionRole_WithInvalidValue_ReturnsFailureResult()
//    {
//        // Arrange
//        const string invalidValue = "Invalid";

//        // Act
//        Result<Champion.ChampionRole> result = Champion.ChampionRole.Create(invalidValue);

//        // Assert
//        result.IsSuccess.Should().BeFalse();
//        result.Errors.Should().ContainItemsAssignableTo<Champion.ChampionRole.ValueOutOfRangeError>();
//    }

//    [Fact]
//    public void ChampionRoleImplicitConversion_WithValidValue_ReturnsChampionRole()
//    {
//        // Arrange
//        const string validValue = "Healer";

//        // Act
//        Champion.ChampionRole role = validValue;

//        // Assert
//        role.Value.Should().Be(validValue);
//    }

//    [Fact]
//    public void ChampionRoleImplicitConversion_WithInvalidValue_ThrowsInvalidOperationException()
//    {
//        // Arrange
//        const string invalidValue = "Invalid";

//        // Act
//        Action act = () => { Champion.ChampionRole role = invalidValue; };

//        // Assert
//        act.Should().Throw<InvalidOperationException>();
//    }

//    [Fact]
//    public void ChampionRoleExplicitConversion_ReturnsStringValue()
//    {
//        // Arrange
//        Champion.ChampionRole role = Champion.ChampionRole.Create("tank").Value;

//        // Act
//        string result = (string)role;

//        // Assert
//        result.Should().Be(role.Value);
//    }

//    [Fact]
//    public void CreateChampionAvatar_ShouldReturnSuccess_WhenUrisAreValid()
//    {
//        // Arrange
//        const string portraitUri = "https://example.com/portrait.png";
//        const string wideUri = "https://example.com/wide.png";

//        // Act
//        Result<Champion.ChampionAvatar> result = Champion.ChampionAvatar.Create(portraitUri, wideUri);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.Value.Portrait.Should().Be(portraitUri);
//        result.Value.PortraitWide.Should().Be(wideUri);
//    }

//    [Theory]
//    [InlineData("", "https://example.com/wide.png")]
//    [InlineData("https://example.com/portrait.png", "")]
//    [InlineData("invalid_uri", "https://example.com/wide.png")]
//    [InlineData("https://example.com/portrait.png", "invalid_uri")]
//    public void CreateChampionAvatar_ShouldReturnFailure_WhenUrisAreInvalid(string portraitUri, string wideUri)
//    {
//        // Act
//        Result<Champion.ChampionAvatar> result = Champion.ChampionAvatar.Create(portraitUri, wideUri);

//        // Assert
//        result.IsFailed.Should().BeTrue();
//        result.HasError<Champion.ChampionAvatar.InvalidUriError>().Should().BeTrue();
//    }
//}

//internal class InvalidNames : IEnumerable<object?[]>
//{
//    public IEnumerator<object?[]> GetEnumerator()
//    {
//        yield return new object?[] { "", typeof(ExceptionalError), typeof(ArgumentException) };
//        yield return new object?[] { null!, typeof(ExceptionalError), typeof(ArgumentNullException) };
//        yield return new object?[] { new string('a', 101), typeof(Champion.ChampionName.GreaterThan100CharactersError), null };
//    }

//    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
//}