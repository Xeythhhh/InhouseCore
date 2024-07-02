using System.Reflection;

namespace WebApp.UnitTests;
/// <summary> Class to reference the WebApp.UnitTests <see cref="Assembly"/> </summary>
public static class WebAppUnitTestsAssembly
{
    /// <summary> A Reference to the WebApp.UnitTests <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(WebAppUnitTestsAssembly).Assembly;
}