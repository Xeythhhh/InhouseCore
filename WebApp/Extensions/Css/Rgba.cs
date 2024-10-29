namespace WebApp.Extensions.Css;

internal record Rgba
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public double A { get; set; }

    public Rgba(string hex, double alpha = 1.0)
    {
        if (string.IsNullOrWhiteSpace(hex) ||
            hex[0] != '#' ||
            hex.Length != 7 && hex.Length != 4)
        {
            throw new ArgumentException("Invalid hex color format");
        }

        A = alpha;

        R = hex.Length == 7
            ? Convert.ToInt32(hex.Substring(1, 2), 16)
            : Convert.ToInt32(new string(hex[1], 2), 16);

        G = hex.Length == 7
            ? Convert.ToInt32(hex.Substring(3, 2), 16)
            : Convert.ToInt32(new string(hex[2], 2), 16);

        B = hex.Length == 7
            ? Convert.ToInt32(hex.Substring(5, 2), 16)
            : Convert.ToInt32(new string(hex[3], 2), 16);
    }

    public override string ToString() => $"rgba({R}, {G}, {B}, {A})";
}
