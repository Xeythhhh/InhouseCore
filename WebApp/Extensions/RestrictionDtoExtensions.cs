using SharedKernel.Contracts.v1.Champions.Dtos;

namespace WebApp.Extensions;

public static class RestrictionDtoExtensions
{
    public static string GetBorderColor(this ChampionRestrictionDto restriction) =>
        restriction.Augment is not null
            ? restriction.Combo is not null

                ? $"""
                   border-image: linear-gradient(to bottom right, {restriction.Augment.AugmentColor}, {restriction.Combo.AugmentColor}) 1!important;
                   """

                : $"""
                   border-color: {restriction.Augment.AugmentColor}!important;
                   """

            : string.Empty;
}
