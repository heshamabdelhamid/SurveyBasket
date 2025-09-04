using SurveyBasket.Abstractions;

namespace SurveyBasket.Errors;

public static class QuestionErrors
{
    public static readonly Error QuestionNotFound =
        new("QuestionNotFound", "Question not found.");

    public static readonly Error DuplicatedQuestionContent =
        new("QuestionAlreadyExists", "Question with the same title already exists.");
}