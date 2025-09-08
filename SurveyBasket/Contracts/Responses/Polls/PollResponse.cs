namespace SurveyBasket.Contracts.Responses.Polls;

public record PollResponse(
    int Id,
    string Title,
    string Summary,
    bool IsPublished,
    DateOnly StartsAt,
    DateOnly EndsAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);