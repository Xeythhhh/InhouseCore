using System.Reflection;

namespace Infrastructure;
/// <summary> Class to reference the Infrastructure <see cref="Assembly"/> </summary>
public static class InfrastructureAssembly
{
    /// <returns> A Reference to the Infrastructure <see cref="Assembly"/> </returns>
    public static Assembly Reference => typeof(InfrastructureAssembly).Assembly;
}
