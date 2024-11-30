using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using SharedKernel.Contracts.v1.Champions.Dtos;

namespace WebApp.Champions.Restrictions;

/// <summary> Represents a UI component displaying a champion restriction with options to update or remove it.</summary>
public partial class Restriction
{
    private readonly string _augmentIconUrl = "https://static.wikia.nocookie.net/battlerite_gamepedia_en/images/e/e2/Rain_Of_Arrows_icon_big.png";

#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable CS0414 // The field 'Restriction._comboIconUrl' is assigned but its value is never used
    private readonly string _comboIconUrl = "https://static.wikia.nocookie.net/battlerite_gamepedia_en/images/e/e2/Rain_Of_Arrows_icon_big.png";
#pragma warning restore CS0414 // The field 'Restriction._comboIconUrl' is assigned but its value is never used
#pragma warning restore IDE0051 // Remove unused private members

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
