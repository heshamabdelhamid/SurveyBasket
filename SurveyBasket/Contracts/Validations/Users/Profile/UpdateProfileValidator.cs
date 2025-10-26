using FluentValidation;
using SurveyBasket.Contracts.Requests.Users.Profile;

namespace SurveyBasket.Contracts.Validations.Users.Profile;

public class UpdateProfileValidator : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Length(3, 50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(3, 50);
    }
}
