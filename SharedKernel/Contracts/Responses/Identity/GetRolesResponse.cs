namespace SharedKernel.Contracts.Responses.Identity;
public sealed record GetRolesResponse(IEnumerable<GetRoleResponse> Roles);