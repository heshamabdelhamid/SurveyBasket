using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Abstractions;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Auhentication.Filter;
using SurveyBasket.Contracts.Requests.Roles;
using SurveyBasket.Services.Roles;
 
namespace SurveyBasket.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IRoleService roleService) : ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    [HttpGet("")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled, CancellationToken cancellationToken)
    {
        var result = await _roleService.GetAllAsync(includeDisabled, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var result = await _roleService.GetAsync(id);
        
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    [HasPermission(Permissions.AddRoles)]
    public async Task<IActionResult> Add([FromBody] RoleRequest request)
    {
        var result = await _roleService.AddAsync(request);
        
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();
    }

    [HttpPut("")]
    [HasPermission(Permissions.UpdateRoles)]
    public async Task<IActionResult> Update([FromBody] UpdateRoleRequest request)
    {
        var result = await _roleService.UpdateAsync(request);
        
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{id}/toggle-status")]
    [HasPermission(Permissions.UpdateRoles)]
    public async Task<IActionResult> ToggleStatus([FromRoute] string id)
    {
        var result = await _roleService.ToggleStatusAsync(id);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
