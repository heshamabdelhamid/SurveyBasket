namespace SurveyBasket.Contracts.Requests.Polls;
public record CreatePollRequest(
        string Title,
        string Description,
        bool Published,
        DateOnly StartsAt,
        DateOnly EndsAt
    );