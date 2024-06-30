using static SharedKernel.Contracts.Responses.Identity.GetPermissionsResponse;

namespace SharedKernel.Contracts.Responses.Identity;

public sealed record GetPermissionsResponse(
    long RoleId,
    string RoleName,
    IEnumerable<RoleClaimDto> RoleClaims)
{
    public sealed record RoleClaimDto(
        long Id,
        long RoleId,
        string Type,
        string Value,
        string Description,
        string Group,
        bool Selected);
}
