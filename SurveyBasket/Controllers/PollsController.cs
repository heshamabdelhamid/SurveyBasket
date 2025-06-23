using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Contracts.Requests.Polls;
using SurveyBasket.Mapping;
using SurveyBasket.Services.Polls;

namespace SurveyBasket.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PollsController(IPollService pollService) : ControllerBase
{
    private readonly IPollService _pollService = pollService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var polls = await _pollService.GetAllAsync(cancellationToken);
        return Ok(polls.ToResponse());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var poll = await _pollService.GetAsync(id, cancellationToken);
        return poll is null ? NotFound() : Ok(poll.ToResponse());
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] CreatePollRequest request, CancellationToken cancellationToken)
    {
        var poll = await _pollService.AddAsync(request.ToEntity(), cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = poll.Id }, poll.ToResponse());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePollRequest request, CancellationToken cancellationToken)
    {
        var isUpdated = await _pollService.UpdateAsync(id, request.ToEntity(), cancellationToken);
        return isUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var isDeleted = await _pollService.DeleteAsync(id, cancellationToken);
        return isDeleted ? NoContent() : NotFound();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> TogglePublishAsync(int id, CancellationToken cancellationToken)
    {
        var isUpdated = await _pollService.TogglePublishAsync(id, cancellationToken);
        return isUpdated ? NoContent() : NotFound();
    }
}