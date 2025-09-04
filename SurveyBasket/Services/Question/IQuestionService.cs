using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Question;
using SurveyBasket.Contracts.Responses.Question;

namespace SurveyBasket.Services.Question;

public interface IQuestionService
{
    Task<Result<QuestionResponse>> AddAsync(int PollId, CreateQuestionRequest questionRequest, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<QuestionResponse>>> GetAllAsync(int PollId, CancellationToken cancellationToken = default);
 
    Task<Result<QuestionResponse>> GetAsync(int PollId, int QuestionId, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(int PollId, int QuestionId, UpdateQuestionRequest questionRequest, CancellationToken cancellationToken = default);

    Task<Result> ToggleStatusAsync(int PollId, int QuestionId, CancellationToken cancellationToken = default);
}