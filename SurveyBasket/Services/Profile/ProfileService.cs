using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Users.Profile;
using SurveyBasket.Contracts.Responses.Users.Profile;
using SurveyBasket.Entities;
using SurveyBasket.Persistence;

namespace SurveyBasket.Services.Profile;

public class ProfileService(UserManager<ApplicationUser> userManager) : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<UserProfileResponse>> GetProfileAsync(string userId)
    {
        var user = await _userManager.Users
            .Where(x => x.Id == userId)
            .ProjectToType<UserProfileResponse>()
            .SingleAsync();

        return Result.Success(user);
    }

    public async Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);

        await _userManager.UpdateAsync(request.Adapt(user)!);

        return Result.Success();
    }

}
