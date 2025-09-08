using SurveyBasket.Abstractions;

namespace SurveyBasket.Errors;

public static class UserErrors
{
    public static readonly Error UserNotFound = 
        new("UserNotFound", "User not found.", StatusCodes.Status404NotFound);

    public static readonly Error InvalidCredentials = 
        new("InvalidCredentials", "Invalid email or password.", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidToken = 
        new("InvalidToken", "Invalid Token/Refresh Token.", StatusCodes.Status401Unauthorized);
}