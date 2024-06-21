using Domain.Errors;

namespace Domain.Champions;

public sealed partial class Champion
{
    public sealed class Errors
    {
        public static string NameIsNotUnique(string? name) => $"Champion name is not unique. {name}";
    }
}
