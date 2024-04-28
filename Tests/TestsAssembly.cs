using System.Reflection;

namespace Tests;
/// <summary> Class to reference the Tests <see cref="Assembly"/> </summary>
public static class TestsAssembly
{
    /// <summary> A Reference to the Tests <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(TestsAssembly).Assembly;
}
