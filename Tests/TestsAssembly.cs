using System.Reflection;

namespace Tests;
/// <summary> Class to reference the Tests <see cref="System.Reflection.Assembly"/> </summary>
public static class TestsAssembly
{
    /// <returns> A Reference to the Tests <see cref="System.Reflection.Assembly"/> </returns>
    public static Assembly Reference => typeof(TestsAssembly).Assembly;
}
