using Microsoft.AspNetCore.Mvc;

namespace SurveyBasket.Abstractions;

public static class ResultExtentions
{
    public static ObjectResult ToProblem(this Result result, int statusCode)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Cannot convert a successful result to a problem result.");

        var problem = Results.Problem(statusCode: statusCode);

        var problemDetails = problem.GetType().
            GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;

        problemDetails!.Extensions = new Dictionary<string, object?>
        {
            { "errors", new[] { result.Error } }
        };

        return new ObjectResult(problemDetails);
    }
}