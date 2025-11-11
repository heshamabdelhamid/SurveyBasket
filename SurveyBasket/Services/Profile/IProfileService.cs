using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Profile;
using SurveyBasket.Contracts.Responses.Profile;

namespace SurveyBasket.Services.Profile;

public interface IProfileService
{
    Task<Result<UserProfileResponse>> GetProfileAsync(string userId);

    Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
}
