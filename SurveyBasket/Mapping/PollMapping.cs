using SurveyBasket.Contracts.Requests;
using SurveyBasket.Contracts.Responses;
using SurveyBasket.Entities;

namespace SurveyBasket.Mapping
{
    public static class PollMapping
    {
        public static PollResponse ToResponse(this Poll poll)
        {
            return new()
            {
                Id = poll.Id,
                Title = poll.Title,
                Notes = poll.Summary,
                Published = poll.IsPublished,
                StartAt = poll.StartsAt,
                EndAt = poll.EndsAt
            };
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
                Summary = request.description,
                IsPublished = request.Published,
                StartsAt =  request.StartsAt,
                EndsAt = request.EndsAt
            };
        }

        public static Poll ToEntity(this UpdatePollRequest request)
        {
            return new()
            {
                Title = request.Title,
                Summary = request.description,
                IsPublished = request.Published,
                StartsAt = request.StartsAt,
                EndsAt = request.EndsAt
            };
        }
    }
}