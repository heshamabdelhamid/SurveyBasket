using SurveyBasket.Entities;

namespace SurveyBasket.Auhentication;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(ApplicationUser user);

    string? ValidateToken(string token);
}