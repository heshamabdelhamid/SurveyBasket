using FluentValidation;
using SurveyBasket.Contracts.Requests.Auth;

namespace SurveyBasket.Contracts.Validations.Auth;

public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordRequest>
{
    public ForgetPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
