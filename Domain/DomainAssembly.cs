using System.Reflection;

namespace Domain;
/// <summary> Class to reference the Domain <see cref="Assembly"/> </summary>
public static class DomainAssembly
{
    /// <returns> A Reference to the Domain <see cref="Assembly"/> </returns>
    public static Assembly Reference => typeof(DomainAssembly).Assembly;
}