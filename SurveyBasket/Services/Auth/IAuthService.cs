using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Auth;
using SurveyBasket.Contracts.Responses.Auth;

namespace SurveyBasket.Services.Auth;

public interface IAuthService
{
    Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);

    Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request);
    
    Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);

    Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
    
    Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);

    Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request, CancellationToken cancellationToken = default);
 
    Task<Result> SendForgotPasswordCodeAsync(ForgetPasswordRequest request);

    Task<Result> ResetPasswordAsync(ResetPasswordRequest request);

}
