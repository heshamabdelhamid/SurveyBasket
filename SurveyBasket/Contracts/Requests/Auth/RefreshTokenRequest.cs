namespace SurveyBasket.Contracts.Requests.Auth;

public record RefreshTokenRequest(
    string Token,
    string RefreshToken
);