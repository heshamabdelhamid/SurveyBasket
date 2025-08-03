namespace SurveyBasket.Contracts.Requests.Auth;

public record LoginRequest(
    string Email,
    string Password
);