using System.Reflection;

namespace SharedKernel;
/// <summary> Class to reference the SharedKernel <see cref="System.Reflection.Assembly"/> </summary>
public static class SharedKernel
{
    /// <returns> A Reference to the SharedKernel <see cref="System.Reflection.Assembly"/> </returns>
    public static Assembly Assembly => typeof(SharedKernel).Assembly;
}