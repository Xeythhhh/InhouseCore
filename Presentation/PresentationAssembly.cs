using System.Reflection;

namespace Presentation;
/// <summary> Class to reference the Presentation <see cref="System.Reflection.Assembly"/> </summary>
public static class PresentationAssembly
{
    /// <returns> A Reference to the Presentation <see cref="System.Reflection.Assembly"/> </returns>
    public static Assembly Reference => typeof(PresentationAssembly).Assembly;
}
