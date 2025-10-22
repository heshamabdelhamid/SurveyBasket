namespace SurveyBasket.Contracts.Requests.Auth;

public record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName
);