namespace SurveyBasket.Contracts.Requests.Polls;
public record CreatePollRequest(
        string Title,
        string Description,
        DateOnly StartsAt,
        DateOnly EndsAt
);