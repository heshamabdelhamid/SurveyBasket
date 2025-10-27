using FluentValidation;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Contracts.Requests.Auth;

namespace SurveyBasket.Contracts.Validations.Auth;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();

        RuleFor(x => x.Code)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
           .NotEmpty()
           .Matches(RegexPatterns.Password)
           .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty()
            .Equal(x => x.NewPassword)
            .WithMessage("Confirm password must match the new password");
    }
}
