using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Abstractions;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Contracts.Requests.Votes;
using SurveyBasket.Extensions;
using SurveyBasket.Services.Question;
using SurveyBasket.Services.Votes;

namespace SurveyBasket.Controllers;

[Route("api/polls/{pollId}/vote")]
[ApiController]
[Authorize]
[Authorize(Roles = DefaultRoles.Member)]
public class VotesController(IQuestionService questionService, IVoteService voteService) : ControllerBase
{
    private readonly IQuestionService _questionService = questionService;
    private readonly IVoteService _voteService = voteService;

    [HttpGet("")]
    public async Task<IActionResult> Start([FromRoute] int pollId, CancellationToken cancellationToken)
    {        
        var result = await _questionService.GetAvailableAsync(pollId, User.GetUserId()!, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    public async Task<IActionResult> AddAsync([FromRoute] int pollId, [FromBody] VoteRequest request, CancellationToken cancellationToken)
    {
        var result = await _voteService.AddAsync(pollId, User.GetUserId()!, request, cancellationToken);
        return result.IsSuccess ? Created() : result.ToProblem();
    }
}