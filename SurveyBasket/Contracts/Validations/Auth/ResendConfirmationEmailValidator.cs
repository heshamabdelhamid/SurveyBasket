using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;

namespace SurveyBasket.Contracts.Validations.Auth;

public class ResendConfirmationEmailValidator : AbstractValidator<ResendConfirmationEmailRequest>
{
    public ResendConfirmationEmailValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("A valid email is required.");
    }
}
