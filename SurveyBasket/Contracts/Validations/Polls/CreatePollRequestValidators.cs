using FluentValidation;
using SurveyBasket.Contracts.Requests.Polls;

namespace SurveyBasket.Contracts.Validations.Polls;

public class CreatePollRequestValidators : AbstractValidator<CreatePollRequest>
{
    public CreatePollRequestValidators()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Length(3, 100)
            .WithMessage("Title must be between {MinLength} and {MaxLength} characters, you entered {PropertyValue} [{TotalLength}].");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Length(5, 1000)
            .WithMessage("Title must be between {MinLength} and {MaxLength} characters, you entered {PropertyValue} [{TotalLength}].");

        RuleFor(x => x.StartsAt)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

        RuleFor(x => x.EndsAt)
            .NotEmpty()
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

        RuleFor(x => x)
            .Must(HasValidDates)
            .WithName(nameof(UpdatePollRequest.EndsAt))
            .WithMessage("{PropertyName} At must be Greater Than or equals Starts At.");
    }
    private static bool HasValidDates(CreatePollRequest request) => request.EndsAt >= request.StartsAt;
}