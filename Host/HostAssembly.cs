using System.Reflection;

namespace Host;
/// <summary> Class to reference the Host <see cref="Assembly"/> </summary>
public static class HostAssembly
{
    /// <summary> A Reference to the Host <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(HostAssembly).Assembly;
}
