using System.Reflection;

namespace Presentation;
/// <summary> Class to reference the Presentation <see cref="Assembly"/> </summary>
public static class PresentationAssembly
{
    /// <summary> A Reference to the Presentation <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(PresentationAssembly).Assembly;
}
