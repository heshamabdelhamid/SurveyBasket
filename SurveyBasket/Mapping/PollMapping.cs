using SurveyBasket.Contracts.Requests.Polls;
using SurveyBasket.Contracts.Responses.Polls;
using SurveyBasket.Entities;

namespace SurveyBasket.Mapping;

public static class PollMapping
{
    public static PollResponse ToResponse(this Poll poll)
    {
        return new(
            poll.Id,
            poll.Title,
            poll.Summary ?? string.Empty,
            poll.IsPublished,
            poll.StartsAt,
            poll.EndsAt);
    }

    public static IEnumerable<PollResponse> ToResponse(this IEnumerable<Poll> polls)
    {
        return polls.Select(p => p.ToResponse());
    }

    public static Poll ToEntity(this CreatePollRequest request)
    {
        return new()
        {
            Title = request.Title,
            Summary = request.Description,
            IsPublished = request.Published,
            StartsAt = request.StartsAt,
            EndsAt = request.EndsAt
        };
    }

    public static Poll ToEntity(this UpdatePollRequest request)
    {
        return new()
        {
            Title = request.Title,
            Summary = request.Description,
            IsPublished = request.Published,
            StartsAt = request.StartsAt,
            EndsAt = request.EndsAt
        };
    }
}