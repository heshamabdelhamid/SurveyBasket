using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Users.Profile;
using SurveyBasket.Contracts.Responses.Users.Profile;

namespace SurveyBasket.Services.Profile;

public interface IProfileService
{
    Task<Result<UserProfileResponse>> GetProfileAsync(string userId);

    Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
}
