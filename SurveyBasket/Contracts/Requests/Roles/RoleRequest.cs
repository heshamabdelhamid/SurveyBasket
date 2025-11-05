namespace SurveyBasket.Contracts.Requests.Roles;

public record RoleRequest(
    string Name,
    IList<string> Permissions
);