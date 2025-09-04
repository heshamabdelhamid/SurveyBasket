
namespace SurveyBasket.Contracts.Requests.Question;

public record UpdateQuestionRequest(
    string Content,
    List<string> Answers
);