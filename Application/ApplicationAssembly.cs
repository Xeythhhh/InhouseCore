using System.Reflection;

namespace Application;
/// <summary> Class to reference the Application <see cref="Assembly"/> </summary>
public static class ApplicationAssembly
{
    /// <returns> A Reference to the Application <see cref="Assembly"/> </returns>
    public static Assembly Reference => typeof(ApplicationAssembly).Assembly;
}