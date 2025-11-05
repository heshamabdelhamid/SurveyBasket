using FluentValidation;
using SurveyBasket.Contracts.Requests.Roles;

namespace SurveyBasket.Contracts.Validations.Roles;

public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
{
    public UpdateRoleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 200);

        RuleFor(x => x.Permissions)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Permissions)
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("You cannot add duplicated permissions for the same role")
            .When(x => x.Permissions != null);
    }
}