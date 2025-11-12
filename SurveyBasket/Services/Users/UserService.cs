using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Abstractions;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Contracts.Requests.Users;
using SurveyBasket.Contracts.Responses.Users;
using SurveyBasket.Entities;
using SurveyBasket.Errors;
using SurveyBasket.Persistence;
using SurveyBasket.Services.Roles;

namespace SurveyBasket.Services.Users;

public class UserService(
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    IRoleService roleService
) : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IRoleService _roleService = roleService;
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<UserResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await (from user in _context.Users
                      join userRoles in _context.UserRoles
                      on user.Id equals userRoles.UserId
                      join r in _context.Roles
                      on userRoles.RoleId equals r.Id into roles
                      where !roles.Any(x => x.Name == DefaultRoles.Member)
                      select new
                      {
                          user.Id,
                          user.FirstName,
                          user.LastName,
                          user.Email,
                          user.IsDisabled,
                          Roles = roles.Select(x => x.Name!).ToList()
                      }
                      )
                      .GroupBy(user => new
                      {
                          user.Id,
                          user.FirstName,
                          user.LastName,
                          user.Email,
                          user.IsDisabled
                      })
                      .Select(user =>
                          new UserResponse
                          (
                              user.Key.Id,
                              user.Key.FirstName,
                              user.Key.LastName,
                              user.Key.Email,
                              user.Key.IsDisabled,
                              user.SelectMany(x => x.Roles)
                          )
                      )
                      .ToListAsync(cancellationToken);
    }

    public async Task<Result<UserResponse>> GetDetailsAsync(string id, CancellationToken cancellationToken = default)
    {
        if (await _userManager.FindByIdAsync(id) is not { } user)
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);

        var userRoles = await _userManager.GetRolesAsync(user);

        //return Result.Success((user, userRoles).Adapt<UserResponse>());

        return Result.Success(new UserResponse
            (
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email!,
                user.IsDisabled,
                userRoles
            ));
    }

    public async Task<Result<UserResponse>> AddUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var EmailIsExisting = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (EmailIsExisting)
            return Result.Failure<UserResponse>(UserErrors.EmailDuplicate);

        var allowedRoles = await _roleService.GetAllAsync(false, cancellationToken);

        if (request.Roles.Except(allowedRoles.Select(x => x.Name)).Any())
            return Result.Failure<UserResponse>(RoleErrors.InvalidRoles);

        var user = request.Adapt<ApplicationUser>();

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRolesAsync(user, request.Roles);
            return Result.Success((user, request.Roles).Adapt<UserResponse>());
        }

        var error = result.Errors.First();

        return Result.Failure<UserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }

    public async Task<Result> UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        var EmailIsExisting = await _context.Users.AnyAsync(x => x.Id != request.Id && x.Email == request.Email, cancellationToken);

        if (EmailIsExisting)
            return Result.Failure(UserErrors.EmailDuplicate);

        var allowedRoles = await _roleService.GetAllAsync(false, cancellationToken);

        if (request.Roles.Except(allowedRoles.Select(x => x.Name)).Any())
            return Result.Failure(RoleErrors.InvalidRoles);

        user = request.Adapt(user);

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            await _context.UserRoles.Where(x => x.UserId == request.Id).ExecuteDeleteAsync(cancellationToken);
            await _userManager.AddToRolesAsync(user, request.Roles);
            return Result.Success();
        }

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }


}
