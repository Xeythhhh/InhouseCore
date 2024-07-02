using System.Reflection;

namespace Api.UnitTests;
/// <summary> Class to reference the Api.UnitTests <see cref="Assembly"/> </summary>
public static class ApiUnitTestsAssembly
{
    /// <summary> A Reference to the Api.UnitTests <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(ApiUnitTestsAssembly).Assembly;
}