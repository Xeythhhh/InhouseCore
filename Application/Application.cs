using System.Reflection;

namespace Application;
/// <summary> Class to reference the Application <see cref="System.Reflection.Assembly"/> </summary>
public static class Application
{
    /// <returns> A Reference to the Application <see cref="System.Reflection.Assembly"/> </returns>
    public static Assembly Assembly => typeof(Application).Assembly;
}