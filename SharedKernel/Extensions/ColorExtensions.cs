using System.Drawing;

namespace SharedKernel.Extensions;
public static class ColorExtensions
{
    public static string ColorToHex(this Color color) =>
        $"#{color.R:X2}{color.G:X2}{color.B:X2}";
}
