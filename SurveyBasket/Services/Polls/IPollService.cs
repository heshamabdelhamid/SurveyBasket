using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Polls;
using SurveyBasket.Contracts.Responses.Polls;

namespace SurveyBasket.Services.Polls;

public interface IPollService
{
    Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PollResponse>> GetCurrentAsync(CancellationToken cancellationToken = default);
    Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<PollResponse>> AddAsync(CreatePollRequest poll, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(int id, UpdatePollRequest poll, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> TogglePublishAsync(int id, CancellationToken cancellationToken = default);
}