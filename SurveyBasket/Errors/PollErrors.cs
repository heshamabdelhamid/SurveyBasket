using SurveyBasket.Abstractions;

namespace SurveyBasket.Errors;

public static class PollErrors
{
    public static readonly Error PollNotFound = 
        new("PollNotFound", "Poll not found.");

    public static readonly Error PollAlreadyExists = 
        new("PollAlreadyExists", "Poll with the same title already exists.");
}