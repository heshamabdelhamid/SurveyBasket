using SurveyBasket.Abstractions;

namespace SurveyBasket.Errors;

public static class VoteErrors
{
    public static readonly Error VoteNotFound = 
        new("VoteNotFound", "Vote not found.", StatusCodes.Status404NotFound);

    public static readonly Error InvalidQuestion =
        new("InvalidQuestion", "Invalid Question.", StatusCodes.Status400BadRequest);

    public static readonly Error DuplicatedVote =
        new("DuplicatedVote", "This user has already voted on the same poll.", StatusCodes.Status409Conflict);
}