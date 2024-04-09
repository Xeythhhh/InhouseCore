using System.Reflection;

namespace Host;
/// <summary> Class to reference the Host <see cref="Assembly"/> </summary>
public static class HostAssembly
{
    /// <returns> A Reference to the Host <see cref="Assembly"/> </returns>
    public static Assembly Reference => typeof(HostAssembly).Assembly;
}
