using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Application.UnitTests")]
namespace Application;
/// <summary> Class to reference the Application <see cref="Assembly"/> </summary>
public static class ApplicationAssembly
{
    /// <summary> A Reference to the Application <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(ApplicationAssembly).Assembly;
}