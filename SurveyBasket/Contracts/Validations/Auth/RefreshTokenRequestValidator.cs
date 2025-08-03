using FluentValidation;
using SurveyBasket.Contracts.Requests.Auth;

namespace SurveyBasket.Contracts.Validations.Auth;

public class RereshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RereshTokenRequestValidator()
    {
        RuleFor(x => x.Token).NotEmpty();

        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
