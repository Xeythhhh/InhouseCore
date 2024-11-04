using Domain.Primitives;

using System.ComponentModel;
using System.Globalization;

namespace Domain.Users;

/// <summary>Strongly-typed Id for <see cref="ApplicationUser"/></summary>
[TypeConverter(typeof(AspNetIdentityIdConverter))]
public sealed record AspNetIdentityId(long Value) : EntityId<ApplicationUser>(Value)
{
    public static explicit operator AspNetIdentityId(string id) =>
        new(long.Parse(id));

    public override string ToString() => base.ToString();
}

/// <summary> Used by aspnetIdentity UserManager.FindByIdAsync(UserId) to convert to and from a string.</summary>
public class AspNetIdentityIdConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) =>
        sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

    public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value) =>
        value is string idValue && long.TryParse(idValue, out long result)
            ? new AspNetIdentityId(result)
            : throw new NotSupportedException($"Cannot convert from {value?.GetType()} to {typeof(AspNetIdentityId)}");

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType) =>
        destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

    public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type? destinationType) =>
        destinationType == typeof(string) && value is AspNetIdentityId id
            ? id.ToString()
            : throw new NotSupportedException($"Cannot convert from {value?.GetType()} to {destinationType}");
}
