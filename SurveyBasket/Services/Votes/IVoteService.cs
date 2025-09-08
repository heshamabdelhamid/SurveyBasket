using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Votes;

namespace SurveyBasket.Services.Votes;

public interface IVoteService
{
    Task<Result> AddAsync(int pollId, string userId, VoteRequest request, CancellationToken cancellationToken = default);
}