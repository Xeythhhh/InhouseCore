using System.Reflection;

namespace Domain.UnitTests;
/// <summary> Class to reference the Domain.UnitTests <see cref="Assembly"/> </summary>
public static class DomainUnitTestsAssembly
{
    /// <returns> A Reference to the Domain.UnitTests <see cref="Assembly"/> </returns>
    public static Assembly Reference => typeof(DomainUnitTestsAssembly).Assembly;
}