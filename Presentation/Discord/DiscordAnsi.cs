namespace Presentation.Discord;

// TODO: research ANSI escape codes and refactor
// can be simplified like \x1b[foreground;background;params decorators[]
// move to SharedKernel
// Should support Console / Discord supported codes
public static class DiscordAnsi
{
    public const string Reset = "\u001b[0m";
    public const string Bold = "\u001b[1m";
    public const string Underline = "\u001b[4m";

    public const string Black = "\u001b[30m";
    public const string Red = "\u001b[31m";
    public const string Green = "\u001b[32m";
    public const string Yellow = "\u001b[33m";
    public const string Blue = "\u001b[34m";
    public const string Magenta = "\u001b[35m";
    public const string Cyan = "\u001b[36m";
    public const string White = "\u001b[37m";
    public const string LightGray = "\u001b[38m";

    public static class Background
    {
        public const string Black = "\u001b[40m";
        public const string Red = "\u001b[41m";
        public const string Green = "\u001b[42m";
        public const string Yellow = "\u001b[43m";
        public const string Blue = "\u001b[44m";
        public const string Magenta = "\u001b[45m";
        public const string Cyan = "\u001b[46m";
        public const string White = "\u001b[47m";
    }
}
