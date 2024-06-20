namespace Domain.Champions;

public sealed partial class Champion
{
    /// <summary>Enumeration of possible champion classes.</summary>
    public enum Classes
    {
        Melee = 0,
        Ranged = 1
    }

    /// <summary>Enumeration of possible champion roles.</summary>
    public enum Roles
    {
        Dps = 0,
        Support = 1
    }
}
