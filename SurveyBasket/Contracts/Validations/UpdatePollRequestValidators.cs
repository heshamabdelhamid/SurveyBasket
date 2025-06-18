using FluentValidation;
using SurveyBasket.Contracts.Requests;

namespace SurveyBasket.Contracts.Validations;

public class UpdatePollRequestValidators : AbstractValidator<UpdatePollRequest>
{
    public UpdatePollRequestValidators()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Length(3, 100)
            .WithMessage("Title must be between {MinLength} and {MaxLength} characters, you entered {PropertyValue} [{TotalLength}].");
            
        RuleFor(x => x.description)
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

    private static bool HasValidDates(UpdatePollRequest request) => request.EndsAt >= request.StartsAt;
}