namespace SurveyBasket.Contracts.Requests.Votes;

public record VoteRequest(
    IEnumerable<VoteAnswerRequest> Answers
);
