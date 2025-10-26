using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Contracts.Requests.Users.Profile;
using SurveyBasket.Extensions;
using SurveyBasket.Services.Profile;

namespace SurveyBasket.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class ProfileController(IProfileService userService) : ControllerBase
{
    private readonly IProfileService _profileService = userService;

    [HttpGet("me")]
    public async Task<IActionResult> GetProfile()
    {
        var result = await _profileService.GetProfileAsync(User.GetUserId()!);
        return Ok(result.Value);
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        await _profileService.UpdateProfileAsync(User.GetUserId()!, request);
        
        return NoContent();
    }

}
