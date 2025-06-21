using SurveyBasket.Contracts.Responses.Auth;

namespace SurveyBasket.Services.Auth;

public interface IAuthService
{
    Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken cancellationToken);
}
