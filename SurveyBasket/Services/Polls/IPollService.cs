using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Polls;
using SurveyBasket.Contracts.Responses.Polls;
using SurveyBasket.Entities;

namespace SurveyBasket.Services.Polls;

public interface IPollService
{
    Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<PollResponse>> AddAsync(CreatePollRequest poll, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, UpdatePollRequest poll, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> TogglePublishAsync(int id, CancellationToken cancellationToken = default);
}