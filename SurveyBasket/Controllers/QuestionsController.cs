using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Abstractions;
using SurveyBasket.Contracts.Requests.Question;
using SurveyBasket.Errors;
using SurveyBasket.Services.Question;

namespace SurveyBasket.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService questionService = questionService;

        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromRoute] int pollId, CancellationToken cancellationToken)
        {
            var result = await questionService.GetAllAsync(pollId, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem(StatusCodes.Status404NotFound);
        }

        [HttpGet("{questionId}")]
        public async Task<IActionResult> Get([FromRoute] int pollId, [FromRoute] int questionId, CancellationToken cancellationToken)
        {
            var result = await questionService.GetAsync(pollId, questionId, cancellationToken);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem(StatusCodes.Status404NotFound);
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromRoute] int pollId, CreateQuestionRequest request,
            CancellationToken cancellationToken)
        {
            var result = await questionService.AddAsync(pollId, request, cancellationToken);

            if (result.IsSuccess)
                return NoContent();

            return result.Error.Equals(QuestionErrors.QuestionNotFound)
                ? result.ToProblem(StatusCodes.Status404NotFound)
                : result.ToProblem(StatusCodes.Status409Conflict);
        }

        [HttpPut("{questionId}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int pollId, [FromRoute] int questionId, [FromBody] UpdateQuestionRequest request, CancellationToken cancellationToken)
        {
            var result = await questionService.UpdateAsync(pollId, questionId, request, cancellationToken);

            if (result.IsSuccess)
                return NoContent();

            return result.Error.Equals(QuestionErrors.QuestionNotFound)
                ? result.ToProblem(StatusCodes.Status404NotFound)
                : result.ToProblem(StatusCodes.Status409Conflict);
        }

        [HttpPatch("{questionId}")]
        public async Task<IActionResult> ToggleStatusAsync([FromRoute] int pollId, [FromRoute] int questionId, CancellationToken cancellationToken)
        {
            var result = await questionService.ToggleStatusAsync(pollId, questionId, cancellationToken);
            return result.IsSuccess
                ? NoContent()
                : result.ToProblem(StatusCodes.Status404NotFound);
        }
    }
}
