using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using SharedKernel.Contracts.v1.Champions.Dtos;

namespace WebApp.Champions.Augments;

/// <summary> Represents a UI component displaying a champion augment with options to update or remove it.</summary>
public partial class Augment
{
    /// <summary> CSS class to apply to the root component of this augment.</summary>
    [Parameter] public string? Class { get; set; }

    /// <summary> The model containing augment properties.</summary>
    [Parameter] public ChampionAugmentDto Model { get; set; }

    /// <summary> Event callback triggered when the augment is edited.</summary>
    /// <remarks>Passes <see cref="MouseEventArgs"/> as an event argument.</remarks>
    [Parameter] public EventCallback<MouseEventArgs> Update { get; set; }

    /// <summary> Event callback triggered when the augment is removed.</summary>
    /// <remarks>Passes <see cref="MouseEventArgs"/> as an event argument.</remarks>
    [Parameter] public EventCallback<MouseEventArgs> Remove { get; set; }
}
