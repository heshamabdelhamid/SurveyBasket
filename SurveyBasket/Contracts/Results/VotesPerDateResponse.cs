namespace SurveyBasket.Contracts.Results;

public record VotesPerDateResponse(
    DateOnly Date,
    int NumberOfVotes
);