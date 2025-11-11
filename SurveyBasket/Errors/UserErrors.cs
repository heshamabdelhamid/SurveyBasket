using SurveyBasket.Abstractions;

namespace SurveyBasket.Errors;

public static class UserErrors
{
    public static readonly Error UserNotFound = 
        new("UserNotFound", "User not found.", StatusCodes.Status404NotFound);

    public static readonly Error DisabledUser = 
        new("DisabledUser", "Disabled User.", StatusCodes.Status401Unauthorized);
    
    public static readonly Error LockedUser = 
        new("LockedUser", "Locked User.", StatusCodes.Status401Unauthorized);
    
    public static readonly Error InvalidCredentials = 
        new("InvalidCredentials", "Invalid email or password.", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidToken = 
        new("InvalidToken", "Invalid Token/Refresh Token.", StatusCodes.Status401Unauthorized);

    public static readonly Error EmailNotConfirmed =
        new("EmailNotConfirmed", "Email address is not confirmed.", StatusCodes.Status401Unauthorized);

    public static readonly Error EmailDuplicate =
        new("EmailDuplicate", "Email address is already in use.", StatusCodes.Status409Conflict);

    public static readonly Error InvalidCode =
        new("InvalidCode", " Code is invalid", StatusCodes.Status400BadRequest);

    public static readonly Error EmailAlreadyConfirmed =
        new("EmailAlreadyConfirmed", " Email Already Confirmed", StatusCodes.Status400BadRequest);

}