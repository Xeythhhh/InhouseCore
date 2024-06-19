using System.Reflection;

namespace Infrastructure.UnitTests;
/// <summary> Class to reference the Infrastructure.UnitTests <see cref="Assembly"/> </summary>
public static class InfrastructureUnitTestsAssembly
{
    /// <summary> A Reference to the Infrastructure.UnitTests <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(InfrastructureUnitTestsAssembly).Assembly;
}