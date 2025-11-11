using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Abstractions;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Contracts.Requests.Roles;
using SurveyBasket.Contracts.Responses.Roles;
using SurveyBasket.Entities;
using SurveyBasket.Errors;
using SurveyBasket.Persistence;

namespace SurveyBasket.Services.Roles;

public class RoleService(
    RoleManager<ApplicationRole> roleManager,
    ApplicationDbContext context
) : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<RoleResponse>> GetAllAsync(bool? includeDisabled, CancellationToken cancellationToken = default)
    {
        return await _roleManager.Roles
            .Where(r => !r.IsDefault && (!r.IsDeleted || includeDisabled == true))
            .ProjectToType<RoleResponse>()
            .ToListAsync(cancellationToken);
    }

    public async Task<Result<RoleDetailsResponse>> GetAsync(string id)
    {
        if (await _roleManager.FindByIdAsync(id) is not { } role)
            return Result.Failure<RoleDetailsResponse>(RoleErrors.RoleNotFound);

        var permissions = await _roleManager.GetClaimsAsync(role);

        RoleDetailsResponse response = new(
            role.Id,
            role.Name!,
            role.IsDefault,
            permissions.Select(p => p.Value)
        );

        return Result.Success(response);
    }

    public async Task<Result<RoleDetailsResponse>> AddAsync(RoleRequest request, CancellationToken cancellationToken = default)
    {
        if (await _roleManager.RoleExistsAsync(request.Name))
            return Result.Failure<RoleDetailsResponse>(RoleErrors.DuplicatedRole);

        if (request.Permissions.Except(Permissions.GetAllPermissions()).Any())
            return Result.Failure<RoleDetailsResponse>(RoleErrors.InvalidPermissions);

        ApplicationRole role = new()
        {
            Name = request.Name,
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        var result = await _roleManager.CreateAsync(role);

        if (result.Succeeded)
        {
            var permissions = request.Permissions
                .Select(x => new IdentityRoleClaim<string>
                {
                    ClaimType = Permissions.Type,
                    ClaimValue = x,
                    RoleId = role.Id
                });

            await _context.AddRangeAsync(permissions, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            RoleDetailsResponse response = new(role.Id, role.Name, role.IsDeleted, request.Permissions);

            return Result.Success(response);
        }

        var error = result.Errors.First();

        return Result.Failure<RoleDetailsResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

    }

    public async Task<Result> UpdateAsync(UpdateRoleRequest request, CancellationToken cancellationToken = default)
    {
        if (await _roleManager.FindByIdAsync(request.Id) is not { } role)
            return Result.Failure<RoleDetailsResponse>(RoleErrors.RoleNotFound);

        var roleIsExists = await _roleManager.Roles.AnyAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken);

        if (roleIsExists)
            return Result.Failure<RoleDetailsResponse>(RoleErrors.DuplicatedRole);

        var allowedPermissions = Permissions.GetAllPermissions();

        if (request.Permissions.Except(allowedPermissions).Any())
            return Result.Failure<RoleDetailsResponse>(RoleErrors.InvalidPermissions);

        role.Name = request.Name;

        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            var currentPermissions = await _context.RoleClaims
                .Where(x => x.RoleId == request.Id && x.ClaimType == Permissions.Type)
                .Select(x => x.ClaimValue)
                .ToListAsync(cancellationToken);

            var newPermissions = request.Permissions.Except(currentPermissions)
                .Select(x => new IdentityRoleClaim<string>
                {
                    ClaimType = Permissions.Type,
                    ClaimValue = x,
                    RoleId = role.Id
                });

            var removedPermissions = currentPermissions.Except(request.Permissions);

            await _context.RoleClaims
                .Where(x => x.RoleId == request.Id && removedPermissions.Contains(x.ClaimValue))
                .ExecuteDeleteAsync(cancellationToken);

            await _context.AddRangeAsync(newPermissions, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }

        var error = result.Errors.First();

        return Result.Failure<RoleDetailsResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }

    public async Task<Result> ToggleStatusAsync(string id)
    {
        if (await _roleManager.FindByIdAsync(id) is not { } role)
            return Result.Failure<RoleDetailsResponse>(RoleErrors.RoleNotFound);

        role.IsDeleted = !role.IsDeleted;

        await _roleManager.UpdateAsync(role);

        return Result.Success();
    }

}
