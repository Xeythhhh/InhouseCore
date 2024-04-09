using System.Reflection;

namespace SharedKernel;
/// <summary> Class to reference the SharedKernel <see cref="Assembly"/> </summary>
public static class SharedKernelAssembly
{
    /// <returns> A Reference to the SharedKernel <see cref="Assembly"/> </returns>
    public static Assembly Reference => typeof(SharedKernelAssembly).Assembly;
}