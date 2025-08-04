namespace SurveyBasket.Contracts.Responses.Polls;

public record PollResponse(
    int Id,
    string Title,
    string Notes,
    bool Published,
    DateOnly StartAt,
    DateOnly EndAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);