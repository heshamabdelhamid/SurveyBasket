namespace SurveyBasket.Contracts.Responses.Auth;

public record AuthResponse(
        string Id,
        string Email,
        string FirstName,
        string LastName,
        string Token,
        int ExpiresAt
    );