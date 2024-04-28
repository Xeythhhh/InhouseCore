using System.Reflection;

namespace Host.Client;
/// <summary> Class to reference the Client <see cref="Assembly"/> </summary>
public static class ClientAssembly
{
    /// <summary> A Reference to the Client <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(ClientAssembly).Assembly;
}
