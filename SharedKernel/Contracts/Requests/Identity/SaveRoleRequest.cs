namespace SharedKernel.Contracts.Requests.Identity;
public sealed record SaveRoleRequest(
    long Id,
    string Name,
    string Description);