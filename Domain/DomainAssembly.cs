using System.Reflection;

namespace Domain;
/// <summary> Class to reference the Domain <see cref="Assembly"/> </summary>
public static class DomainAssembly
{
    /// <summary>A Reference to the Domain <see cref="Assembly"/></summary>
    public static Assembly Reference => typeof(DomainAssembly).Assembly;
}