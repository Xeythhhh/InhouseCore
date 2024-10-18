using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Build.UnitTests")]
namespace Build;
/// <summary> Class to reference the Build <see cref="Assembly"/> </summary>
public static class BuildAssembly
{
    /// <summary> A Reference to the Build <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(BuildAssembly).Assembly;
}