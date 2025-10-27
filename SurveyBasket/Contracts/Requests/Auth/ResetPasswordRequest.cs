namespace SurveyBasket.Contracts.Requests.Auth;

public record ResetPasswordRequest(
    string Email,
    string Code,
    string NewPassword,
    string ConfirmNewPassword
);