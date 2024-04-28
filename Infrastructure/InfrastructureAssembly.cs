using System.Reflection;

namespace Infrastructure;
/// <summary> Class to reference the Infrastructure <see cref="Assembly"/> </summary>
public static class InfrastructureAssembly
{
    /// <summary> A Reference to the Infrastructure <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(InfrastructureAssembly).Assembly;
}
