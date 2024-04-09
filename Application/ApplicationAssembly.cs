using System.Reflection;

namespace Application;
/// <summary> Class to reference the Application <see cref="System.Reflection.Assembly"/> </summary>
public static class ApplicationAssembly
{
    /// <returns> A Reference to the Application <see cref="System.Reflection.Assembly"/> </returns>
    public static Assembly Reference => typeof(ApplicationAssembly).Assembly;
}