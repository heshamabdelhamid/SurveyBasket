using SurveyBasket.Contracts.Responses.Answer;

namespace SurveyBasket.Contracts.Responses.Question;

public record QuestionResponse(
    int Id,
    string Content,
    IEnumerable<AnswerResponse> Answers
);
