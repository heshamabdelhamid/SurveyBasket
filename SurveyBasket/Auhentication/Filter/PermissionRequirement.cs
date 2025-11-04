using Microsoft.AspNetCore.Authorization;

namespace SurveyBasket.Auhentication.Filter;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
