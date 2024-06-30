namespace SharedKernel.Contracts.Responses.Identity;

public sealed record GetRoleClaimResponse(
    long Id,
    long RoleId,
    string Type,
    string Value,
    string Description,
    string Group,
    bool Selected);