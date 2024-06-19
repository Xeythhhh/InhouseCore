namespace Presentation.Discord;

/// <summary>Utility class for generating ANSI escape sequences for text styling in console outputs</summary>
public static class AnsiHelper
{
    private const string AnsiEscape = "\x1b[";

    private static readonly Dictionary<Color, string> ColorCodes = new()
    {
        { Color.Black, "30" },
        { Color.Red, "31" },
        { Color.Green, "32" },
        { Color.Yellow, "33" },
        { Color.Blue, "34" },
        { Color.Magenta, "35" },
        { Color.Cyan, "36" },
        { Color.White, "37" },
        { Color.LightGray, "38" },
        { Color.BackgroundBlack, "40" },
        { Color.BackgroundRed, "41" },
        { Color.BackgroundGreen, "42" },
        { Color.BackgroundYellow, "43" },
        { Color.BackgroundBlue, "44" },
        { Color.BackgroundMagenta, "45" },
        { Color.BackgroundCyan, "46" },
        { Color.BackgroundWhite, "47" }
    };

    private static readonly Dictionary<Decorator, string> DecoratorCodes = new()
    {
        { Decorator.Reset, "0" },
        { Decorator.Bold, "1" },
        { Decorator.Underline, "4" }
    };

    /// <summary>Enumeration of ANSI color codes for foreground and background colors</summary>
    public enum Color
    {
        Black,
        Red,
        Green,
        Yellow,
        Blue,
        Magenta,
        Cyan,
        White,
        LightGray,
        BackgroundBlack,
        BackgroundRed,
        BackgroundGreen,
        BackgroundYellow,
        BackgroundBlue,
        BackgroundMagenta,
        BackgroundCyan,
        BackgroundWhite
    }

    /// <summary>Enumeration of ANSI decorators for text styling</summary>
    [Flags]
    public enum Decorator
    {
        /// <summary>No decorators.</summary>
        None = 0,
        /// <summary>Bold text.</summary>
        Bold = 1,
        /// <summary>Underlined text.</summary>
        Underline = 2,
        /// <summary>Reset to default styling.</summary>
        Reset = 4
    }

    /// <summary>Generates an ANSI escape sequence based on the specified decorators and colors</summary>
    /// <param name="decorators">The ANSI decorators to apply.</param>
    /// <param name="colors">The ANSI colors to apply.</param>
    /// <returns>An ANSI escape sequence string.</returns>
    public static string GetAnsiEscape(Decorator decorators, params Color[] colors)
    {
        if (decorators.HasFlag(Decorator.Reset)) return $"{AnsiEscape}0m";

        List<string> codes = new();
        foreach (Color color in colors)
        {
            codes.Add(ColorCodes[color]);
        }

        if (decorators.HasFlag(Decorator.Bold))
        {
            codes.Add(DecoratorCodes[Decorator.Bold]);
        }

        if (decorators.HasFlag(Decorator.Underline))
        {
            codes.Add(DecoratorCodes[Decorator.Underline]);
        }

        return $"{AnsiEscape}{string.Join(";", codes)}m";
    }
}
