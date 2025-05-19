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
                Notes = poll.Description
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
                Description = request.Description
            };
        }
    }
}