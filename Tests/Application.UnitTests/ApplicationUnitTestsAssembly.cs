using System.Reflection;

namespace Application.UnitTests;
/// <summary> Class to reference the Application.UnitTests <see cref="Assembly"/> </summary>
public static class ApplicationUnitTestsAssembly
{
    /// <summary> A Reference to the Application.UnitTests <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(ApplicationUnitTestsAssembly).Assembly;
}