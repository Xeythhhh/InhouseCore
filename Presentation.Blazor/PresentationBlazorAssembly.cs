using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Presentation.Blazor.UnitTests")]
namespace Presentation.Blazor;

/// <summary> Class to reference the Presentation.Blazor <see cref="Assembly"/> </summary>
public static class PresentationBlazorAssembly
{
    /// <summary> A Reference to the Presentation.Blazor <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(PresentationBlazorAssembly).Assembly;
}