using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Results;

namespace SurveyBasket.Services.Results;

public interface IResultService
{
    Task<Result<PollVotesResponse>> GetPollVotesAsync(int PollId, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<VotesPerDateResponse>>> GetVotesPerDayAsync(int PollId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesPerQuestionAsync(int PollId, CancellationToken cancellationToken = default);
}
