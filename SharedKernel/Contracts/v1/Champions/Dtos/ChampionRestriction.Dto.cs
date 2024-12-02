namespace SharedKernel.Contracts.v1.Champions.Dtos
{
    public sealed class ChampionRestrictionDto
    {
        public long RestrictionId { get; set; }
        public long RestrictedAugmentId { get; set; }
        public long? RestrictedComboAugmentId { get; set; }
        public string Reason { get; set; }
        public string? AugmentName { get; set; }
        public string? ComboName { get; set; }

        public ChampionRestrictionDto() { }

        public ChampionRestrictionDto(
            long restrictionId,
            long restrictedAugmentId,
            long? restrictedComboAugmentId,
            string reason)
        {
            RestrictionId = restrictionId;
            RestrictedAugmentId = restrictedAugmentId;
            RestrictedComboAugmentId = restrictedComboAugmentId;
            Reason = reason;
        }

        public ChampionAugmentDto? Augment { get; set; }
        public ChampionAugmentDto? Combo { get; set; }
    }
}
