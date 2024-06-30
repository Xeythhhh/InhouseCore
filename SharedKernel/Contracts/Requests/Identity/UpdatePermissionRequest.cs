using static SharedKernel.Contracts.Requests.Identity.UpdatePermissionRequest;

namespace SharedKernel.Contracts.Requests.Identity;

public sealed record UpdatePermissionRequest(
    long RoleId,
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
