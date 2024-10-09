using CleanArchitecture.Domain.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Infrastructure.Authentication;


public class PermissionRequeriment : IAuthorizationRequirement
{
    public PermissionRequeriment(string? permission)
    {
        Permission = permission;
    }

    public string Permission { get; }
}