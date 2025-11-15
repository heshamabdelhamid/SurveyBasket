using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Abstractions;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Auhentication.Filter;
using SurveyBasket.Contracts.Common;
using SurveyBasket.Contracts.Requests.Question;
using SurveyBasket.Services.Question;

namespace SurveyBasket.Controllers;

[Route("api/polls/{pollId}/[controller]")]
[ApiController]
[Authorize]
public class QuestionsController(IQuestionService questionService) : ControllerBase
{
    private readonly IQuestionService questionService = questionService;

    [HttpGet("")]
    [HasPermission(Permissions.GetQuestions)]
    public async Task<IActionResult> GetAll([FromRoute] int pollId, [FromQuery] RequestFilters filters, CancellationToken cancellationToken)
    {
        var result = await questionService.GetAllAsync(pollId, filters, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpGet("{questionId}")]
    [HasPermission(Permissions.GetQuestions)]
    public async Task<IActionResult> Get([FromRoute] int pollId, [FromRoute] int questionId, CancellationToken cancellationToken)
    {
        var result = await questionService.GetAsync(pollId, questionId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    [HasPermission(Permissions.AddQuestions)]
    public async Task<IActionResult> Add([FromRoute] int pollId, CreateQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var result = await questionService.AddAsync(pollId, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{questionId}")]
    [HasPermission(Permissions.GetQuestions)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int pollId, [FromRoute] int questionId, [FromBody] UpdateQuestionRequest request, CancellationToken cancellationToken)
    {
        var result = await questionService.UpdateAsync(pollId, questionId, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPatch("{questionId}")]
    [HasPermission(Permissions.UpdateQuestions)]
    public async Task<IActionResult> ToggleStatusAsync([FromRoute] int pollId, [FromRoute] int questionId, CancellationToken cancellationToken)
    {
        var result = await questionService.ToggleStatusAsync(pollId, questionId, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}