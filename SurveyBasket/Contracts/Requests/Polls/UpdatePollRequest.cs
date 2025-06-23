namespace SurveyBasket.Contracts.Requests.Polls;

public record UpdatePollRequest
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool Published { get; init; }
    public DateOnly StartsAt { get; init; }
    public DateOnly EndsAt { get; init; }

}