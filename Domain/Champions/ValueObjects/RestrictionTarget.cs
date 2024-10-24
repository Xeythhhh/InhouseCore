using Domain.Errors;
using Domain.Primitives;

using SharedKernel.Primitives.Reasons;
using SharedKernel.Primitives.Result;

namespace Domain.Champions.ValueObjects;

/// <summary>Value object for the Restriction's Target.</summary>
public sealed partial record RestrictionTarget : ValueObject<string>
{
    private const string separator = ";"; // TODO control the separator from configuration

    public TargetName Name { get; set; }
    public TargetIdentifier Identifier { get; set; }

    private RestrictionTarget(string name, string identifier)
    {
        Name = TargetName.Create(name).Value;
        Identifier = TargetIdentifier.Create(identifier).Value;
        Value = $"{name}{separator}{identifier}";
    }

    /// <summary>Creates a new <see cref="RestrictionTarget"/> instance.</summary>
    /// <param name="value">The target value to create.</param>
    /// <returns>A result containing the <see cref="RestrictionTarget"/> if successful.</returns>
    public static Result<RestrictionTarget> Create(string? value) =>
        Result.Try(() => value?.Trim())
            .Ensure(target => !string.IsNullOrEmpty(target), new DomainErrors.NullOrEmptyError())
            .Bind(GetNameAndIdentifier!)
            .Map(target => new RestrictionTarget(target.Item1, target.Item2));

    private static Result<(string, string)> GetNameAndIdentifier(string target) =>
        Result.Ok(target.Split(separator))
            .Ensure(values => values.Length == 2, new InvalidFormatError(target))
            .Map(values => (values[0], values[1]));

    /// <summary>Gets the atomic values of the value object.</summary>
    /// <returns>An enumerable of atomic values.</returns>
    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
        yield return Name;
        yield return Identifier;
    }

    /// <summary>Implicit conversion from string to <see cref="RestrictionTarget"/>.</summary>
    /// <param name="value">The string value to convert.</param>
    public static implicit operator RestrictionTarget(string value)
    {
        Result<RestrictionTarget> result = Create(value);
        return result.IsSuccess ? result.Value
            : throw DomainErrors.InvalidValueError.Exception();
    }

    public class InvalidFormatError(string? target) : Error($"Target Format is invalid. '{target}'");
}
