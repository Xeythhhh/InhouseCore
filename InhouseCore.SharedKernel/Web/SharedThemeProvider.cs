using Blazorise;

namespace InhouseCore.SharedKernel.Web;
public static class SharedThemeProvider
{
    public static Theme Theme => new()
    {
        BarOptions = new()
        {
            HorizontalHeight = "72px"
        },
        ColorOptions = new()
        {
            Primary = "#b3005d",
            Secondary = "#026A71",
            Success = "#428734",
            Info = "#6C808C",
            Warning = "#EB9118",
            Danger = "#DF280B",
            Light = "#808080",
            Dark = "#202020",
        },
        BackgroundOptions = new()
        {
            Primary = "#b3005d",
            Secondary = "#026A71",
            Success = "#428734",
            Info = "#6C808C",
            Warning = "#EB9118",
            Danger = "#DF280B",
            Light = "#808080",
            Dark = "#202020",
        },
        InputOptions = new()
        {
            CheckColor = "#b3005d",
        }
    };
}
