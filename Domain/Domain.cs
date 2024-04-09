using System.Reflection;

namespace Domain;
/// <summary> Class to reference the Domain <see cref="System.Reflection.Assembly"/> </summary>
public static class Domain
{
    /// <returns> A Reference to the Domain <see cref="System.Reflection.Assembly"/> </returns>
    public static Assembly Assembly => typeof(Domain).Assembly;
}