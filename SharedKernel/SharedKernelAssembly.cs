using System.Reflection;

namespace SharedKernel;
/// <summary> Class to reference the SharedKernel <see cref="Assembly"/> </summary>
public static class SharedKernelAssembly
{
    /// <summary> A Reference to the SharedKernel <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(SharedKernelAssembly).Assembly;
}