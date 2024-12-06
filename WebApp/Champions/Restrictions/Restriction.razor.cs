using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using SharedKernel.Contracts.v1.Champions.Dtos;

namespace WebApp.Champions.Restrictions;

/// <summary> Represents a UI component displaying a champion restriction with options to update or remove it.</summary>
public partial class Restriction
{
    /// <summary> CSS classes to apply to the root component of this restriction.</summary>
    [Parameter] public string? Class { get; set; }

    /// <summary> Style to apply to the content of this restriction.</summary>
    [Parameter] public string? Style { get; set; }

    /// <summary> The model containing restriction details, such as augment target, combo, and reason.</summary>
    [Parameter] public ChampionRestrictionDto Model { get; set; }

    /// <summary> Event callback triggered when the restriction is edited.</summary>
    /// <remarks>Passes <see cref="MouseEventArgs"/> as an event argument.</remarks>
    [Parameter] public EventCallback<MouseEventArgs> Update { get; set; } = default!;

    /// <summary> Event callback triggered when the restriction is removed.</summary>
    /// <remarks>Passes <see cref="MouseEventArgs"/> as an event argument.</remarks>
    [Parameter] public EventCallback<MouseEventArgs> Remove { get; set; } = default!;
}
