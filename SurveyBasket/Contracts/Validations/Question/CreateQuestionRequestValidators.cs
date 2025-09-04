using FluentValidation;
using SurveyBasket.Contracts.Requests.Question;

namespace SurveyBasket.Contracts.Validations.Question;

public class CreateQuestionRequestValidators : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionRequestValidators()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .Length(3,1000);

        RuleFor(x => x.Answers)
            .NotNull();

        RuleFor(x => x.Answers)
            .Must(x => x.Count > 1)
            .WithMessage("At least two answers are required.")
            .When(x => x.Answers != null);
         
        RuleFor(x => x.Answers)
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("You can not add dubplicated answer for the same questions")
            .When(x => x.Answers != null);
    }
}