using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SharedKernel.UnitTests")]
namespace SharedKernel.UnitTests;
/// <summary> Class to reference the SharedKernel.UnitTests <see cref="Assembly"/> </summary>
public static class SharedKernelUnitTestsAssembly
{
    /// <summary> A Reference to the SharedKernel.UnitTests <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(SharedKernelUnitTestsAssembly).Assembly;
}