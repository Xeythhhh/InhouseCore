using System.Reflection;

namespace Presentation;
/// <summary> Class to reference the Infrastructure <see cref="System.Reflection.Assembly"/> </summary>
public static class Infrastructure
{
    /// <returns> A Reference to the Infrastructure <see cref="System.Reflection.Assembly"/> </returns>
    public static Assembly Assembly => typeof(Infrastructure).Assembly;
}
