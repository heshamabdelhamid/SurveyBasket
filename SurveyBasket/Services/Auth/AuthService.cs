using Microsoft.AspNetCore.Identity;
using SurveyBasket.Auhentication;
using SurveyBasket.Contracts.Responses.Auth;
using SurveyBasket.Entities;

namespace SurveyBasket.Services.Auth;

public class AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<AuthResponse?> GetTokenAsync(string email, string password,
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return null;

        var isValidPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!isValidPassword)
            return null;

        var (token, expiresIn) = _jwtProvider.GenerateToken(user);

        return new(
            user.Id,
            user.Email ?? string.Empty,
            user.FirstName,
            user.LastName,
            token,
            expiresIn);
    }
}