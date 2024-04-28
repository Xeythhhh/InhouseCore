using System.Reflection;

namespace Domain.UnitTests;
/// <summary> Class to reference the Domain.UnitTests <see cref="Assembly"/> </summary>
public static class DomainUnitTestsAssembly
{
    /// <summary> A Reference to the Domain.UnitTests <see cref="Assembly"/> </summary>
    public static Assembly Reference => typeof(DomainUnitTestsAssembly).Assembly;
}