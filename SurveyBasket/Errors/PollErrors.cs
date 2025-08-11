using SurveyBasket.Abstractions;

namespace SurveyBasket.Errors;

public static class PollErrors
{
    public static readonly Error PollNotFound = new("PollNotFound", "Poll not found.");
}