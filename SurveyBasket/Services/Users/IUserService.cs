using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Users;
using SurveyBasket.Contracts.Responses.Users;

namespace SurveyBasket.Services.Users;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<Result<UserResponse>> GetDetailsAsync(string id, CancellationToken cancellationToken = default);

    Task<Result<UserResponse>> AddUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
}