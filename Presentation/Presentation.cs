using System.Reflection;

namespace Presentation;
/// <summary> Class to reference the Presentation <see cref="System.Reflection.Assembly"/> </summary>
public static class Presentation
{
    /// <returns> A Reference to the Presentation <see cref="System.Reflection.Assembly"/> </returns>
    public static Assembly Assembly => typeof(Presentation).Assembly;
}
