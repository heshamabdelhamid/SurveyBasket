using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Roles;
using SurveyBasket.Contracts.Responses.Roles;

namespace SurveyBasket.Services.Roles;

public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAllAsync(bool? includeDisabled = false, CancellationToken cancellationToken = default);

    Task<Result<RoleDetailsResponse>> GetAsync(string Id);

    Task<Result<RoleDetailsResponse>> AddAsync(RoleRequest request, CancellationToken cancellationToken = default);

    Task<Result> UpdateAsync(UpdateRoleRequest request, CancellationToken cancellationToken = default);

    Task<Result> ToggleStatusAsync(string id);
}