namespace SurveyBasket.Contracts.Responses.Roles;

public record RoleResponse(
    string Id,
    string Name,
    bool   IsDeleted
);