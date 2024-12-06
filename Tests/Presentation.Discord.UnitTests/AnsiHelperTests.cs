//using FluentAssertions;

//namespace Presentation.Discord.UnitTests;

//public class AnsiHelperTests
//{
//    [Fact]
//    public void Test_GetAnsiEscape_NoDecorators()
//    {
//        // Arrange
//        const string expectedEscapeSequence = "\x1b[31m";

//        // Act
//        string escapeSequence = AnsiHelper.GetAnsiEscape(
//            AnsiHelper.Decorator.None,
//            AnsiHelper.Color.Red);

//        // Assert
//        escapeSequence.Should().Be(expectedEscapeSequence);
//    }

//    [Fact]
//    public void Test_GetAnsiEscape_BoldAndUnderline()
//    {
//        // Arrange
//        const string expectedEscapeSequence = "\x1b[34;1;4m";

//        // Act
//        string escapeSequence = AnsiHelper.GetAnsiEscape(
//            AnsiHelper.Decorator.Bold | AnsiHelper.Decorator.Underline,
//            AnsiHelper.Color.Blue);

//        // Assert
//        escapeSequence.Should().Be(expectedEscapeSequence);
//    }

//    [Fact]
//    public void Test_GetAnsiEscape_Reset()
//    {
//        // Arrange
//        const string expectedEscapeSequence = "\x1b[0m";

//        // Act
//        string escapeSequence = AnsiHelper.GetAnsiEscape(AnsiHelper.Decorator.Reset);

//        // Assert
//        escapeSequence.Should().Be(expectedEscapeSequence);
//    }

//    [Fact]
//    public void Test_GetAnsiEscape_Reset_ShouldIgnoreColors()
//    {
//        // Arrange
//        const string expectedEscapeSequence = "\x1b[0m";

//        // Act
//        string escapeSequence = AnsiHelper.GetAnsiEscape(
//            AnsiHelper.Decorator.Reset,
//            AnsiHelper.Color.Magenta);

//        // Assert
//        escapeSequence.Should().Be(expectedEscapeSequence);
//    }

//    [Fact]
//    public void Test_GetAnsiEscape_MultipleColors()
//    {
//        // Arrange
//        const string expectedEscapeSequence = "\x1b[32;43m";

//        // Act
//        string escapeSequence = AnsiHelper.GetAnsiEscape(
//            AnsiHelper.Decorator.None,
//            AnsiHelper.Color.Green, AnsiHelper.Color.BackgroundYellow);

//        // Assert
//        escapeSequence.Should().Be(expectedEscapeSequence);
//    }

//    // Add more tests as needed
//}
