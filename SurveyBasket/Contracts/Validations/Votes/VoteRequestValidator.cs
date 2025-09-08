using FluentValidation;
using SurveyBasket.Contracts.Requests.Votes;

namespace SurveyBasket.Contracts.Validations.Votes;

public class VoteRequestValidator : AbstractValidator<VoteRequest>
{
    public VoteRequestValidator()
    {
        RuleFor(x => x.Answers)
            .NotEmpty();

        RuleForEach(x => x.Answers)
            .SetInheritanceValidator(
                v => v.Add(new VoteAnswerRequestValidator())
            );
    }
}