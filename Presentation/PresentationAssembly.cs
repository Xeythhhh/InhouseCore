using System.Reflection;

namespace Presentation;
/// <summary> Class to reference the Presentation <see cref="Assembly"/> </summary>
public static class PresentationAssembly
{
    /// <returns> A Reference to the Presentation <see cref="Assembly"/> </returns>
    public static Assembly Reference => typeof(PresentationAssembly).Assembly;
}
