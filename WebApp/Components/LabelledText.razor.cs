using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace WebApp.Components;
public partial class LabelledText
{
    /// <summary> Sets the typography style applied to both Label and Text if individual styles are not specified.</summary>
    [Parameter] public Typo Typo { get; set; } = Typo.body2;

    /// <summary> Overrides the typography style specifically for the Label.</summary>
    /// <remarks> If set, this will override the value of <see cref="Typo"/> for the Label only.</remarks>
    [Parameter] public Typo? TypoLabel { get; set; } = Typo.body1;

    /// <summary> Overrides the typography style specifically for the Text.</summary>
    /// <remarks> If set, this will override the value of <see cref="Typo"/> for the Text only.</remarks>
    [Parameter] public Typo? TypoText { get; set; }

    /// <summary> The text displayed as the Label.</summary>
    [Parameter] public string Label { get; set; } = "Label";

    /// <summary> The main content text displayed beside the Label.</summary>
    [Parameter] public string Text { get; set; } = "Text";

    /// <summary> The main content text alignment.</summary>
    [Parameter] public Align TextAlign { get; set; } = Align.Inherit;
}