namespace SurveyBasket.Contracts.Requests.Polls;

public record UpdatePollRequest(
    string Title,
    string Description,
    DateOnly StartsAt,
    DateOnly EndsAt 
);