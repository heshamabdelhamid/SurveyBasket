using Microsoft.AspNetCore.Authorization;

namespace SurveyBasket.Auhentication.Filter;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}
