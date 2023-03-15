using System.Reflection;

namespace InhouseCore.Domain;
public sealed class DomainAssembly
{
    public static Assembly Value => typeof(DomainAssembly).Assembly;
}
