using FluentValidation;
using SurveyBasket.Abstractions.Consts;
using SurveyBasket.Contracts.Requests.Auth;

namespace SurveyBasket.Contracts.Validations.Auth;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase")
            .NotEqual(x => x.CurrentPassword)
            .WithMessage("New password cannot be same as the current password");

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty()
            .Equal(x => x.NewPassword)
            .WithMessage("Confirm password must match the new password");
    }

}
