using Microsoft.AspNetCore.Identity;
using SurveyBasket.Abstractions;
using SurveyBasket.Auhentication;
using SurveyBasket.Contracts.Responses.Auth;
using SurveyBasket.Entities;
using SurveyBasket.Errors;
using System.Security.Cryptography;

namespace SurveyBasket.Services.Auth;

public class AuthService(UserManager<ApplicationUser> userManager, IJwtProvider jwtProvider) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    private readonly int _refreshTokenExpiryDays = 14;

    public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken)
    {
        // Check User
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.UserNotFound);

        // Check Password
        var isValidPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!isValidPassword)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        // Generate Token
        var (token, expiresIn) = _jwtProvider.GenerateToken(user);

        // Generate Refresh Token
        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = refreshTokenExpiration
        });
        
        await _userManager.UpdateAsync(user);

        return Result.Success(
            new AuthResponse(
                user.Id,
                user.Email ?? string.Empty,
                user.FirstName,
                user.LastName,
                token,
                expiresIn,
                refreshToken,
                refreshTokenExpiration
            )
        );
    }

    public async Task<Result<AuthResponse>>  GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken)
    { 
        var userId = _jwtProvider.ValidateToken(token);

        if (userId is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidToken);

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidToken);

        var existingRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

        if (existingRefreshToken is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidToken);

        existingRefreshToken.RevokedOn = DateTime.UtcNow;

        // Generate Token
        var (newToken, expiresIn) = _jwtProvider.GenerateToken(user);

        // Generate Refresh Token
        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration
        });

        await _userManager.UpdateAsync(user);

        return Result.Success(
            new AuthResponse(
                user.Id,
                user.Email ?? string.Empty,
                user.FirstName,
                user.LastName,
                newToken,
                expiresIn,
                newRefreshToken,
                refreshTokenExpiration
            )
        );
    }

    public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(token);

        if (userId is null)
            return Result.Failure<AuthResponse>(UserErrors.UserNotFound);

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.UserNotFound);

        var existingRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

        if (existingRefreshToken is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidToken);

        existingRefreshToken.RevokedOn = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);
        return Result.Success();
    }

    private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
}
