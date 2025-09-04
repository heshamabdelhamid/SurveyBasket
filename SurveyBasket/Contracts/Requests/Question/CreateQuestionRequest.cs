namespace SurveyBasket.Contracts.Requests.Question;

public record CreateQuestionRequest(
    int PollId,
    string Content,
    List<string> Answers
);