namespace SharedKernel.Contracts.v1.Champions.Dtos
{
    public sealed class ChampionRestrictionDto
    {
        public string RestrictionId { get; set; }
        public string RestrictedAugmentId { get; set; }
        public string? RestrictedComboAugmentId { get; set; }
        public string Reason { get; set; }

        public ChampionRestrictionDto() { }

        public ChampionRestrictionDto(
            string restrictionId,
            string restrictedAugmentId,
            string? restrictedComboAugmentId,
            string reason)
        {
            RestrictionId = restrictionId;
            RestrictedAugmentId = restrictedAugmentId;
            RestrictedComboAugmentId = restrictedComboAugmentId;
            Reason = reason;
        }
    }
}
