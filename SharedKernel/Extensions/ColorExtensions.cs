using System.Drawing;

namespace SharedKernel.Extensions;
public static class ColorExtensions
{
    /// <summary>Converts color to string representation of the hex code.</summary>
    /// <remarks>Ignores the alpha channel!</remarks>
    public static string ColorToHex(this Color color) =>
        $"#{color.R:X2}{color.G:X2}{color.B:X2}";
}
