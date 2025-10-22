using FluentValidation;
using SurveyBasket.Contracts.Requests.Auth;

namespace SurveyBasket.Contracts.Validations.Auth;

public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Code)
            .NotEmpty();
    }
}
