namespace SurveyBasket.Contracts.Requests.Users;

public record UpdateUserRequest(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    IList<string> Roles
);
