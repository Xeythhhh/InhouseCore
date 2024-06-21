namespace SharedKernel.Extensions;
public static class TypeExtensions
{
    public static bool IsAssignableTo(this Type source, Type target)
    {
        if (source == target) return true;

        foreach (Type type in source.GetInterfaces())
            if (type == target) return true;

        return source.BaseType?.IsAssignableTo(target) == true;
    }
}
