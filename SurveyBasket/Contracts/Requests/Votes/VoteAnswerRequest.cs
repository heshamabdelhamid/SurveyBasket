namespace SurveyBasket.Contracts.Requests.Votes;

public record VoteAnswerRequest(
    int QuestionId,
    int AnswerId
);
