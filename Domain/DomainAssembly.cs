using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Domain.UnitTests")]
namespace Domain;
/// <summary>Class to reference the Domain <see cref="Assembly"/></summary>
public static class DomainAssembly
{
    /// <summary>A Reference to the Domain <see cref="Assembly"/></summary>
    public static Assembly Reference => typeof(DomainAssembly).Assembly;
}