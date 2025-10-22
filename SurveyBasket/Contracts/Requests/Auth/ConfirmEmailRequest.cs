namespace SurveyBasket.Contracts.Requests.Auth;

public record ConfirmEmailRequest(
    string UserId,
    string Code
);
