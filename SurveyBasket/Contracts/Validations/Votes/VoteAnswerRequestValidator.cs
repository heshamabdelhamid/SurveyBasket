using FluentValidation;
using SurveyBasket.Contracts.Requests.Votes;

namespace SurveyBasket.Contracts.Validations.Votes;

public class VoteAnswerRequestValidator : AbstractValidator<VoteAnswerRequest>
{
    public VoteAnswerRequestValidator()
    {
        RuleFor(x => x.QuestionId)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.AnswerId)
            .GreaterThan(0);
    }
}