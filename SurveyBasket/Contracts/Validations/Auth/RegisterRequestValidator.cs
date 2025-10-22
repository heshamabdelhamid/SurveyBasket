using FluentValidation;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Contracts.Requests.Auth;

namespace SurveyBasket.Contracts.Validations.Auth;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(RegexPatterns.Password);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Length(3,100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(3, 100);
    }
}
