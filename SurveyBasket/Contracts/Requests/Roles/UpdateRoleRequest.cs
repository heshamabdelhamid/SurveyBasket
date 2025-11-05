namespace SurveyBasket.Contracts.Requests.Roles;

public record UpdateRoleRequest(
    string Id,
    string Name,
    IList<string> Permissions
);