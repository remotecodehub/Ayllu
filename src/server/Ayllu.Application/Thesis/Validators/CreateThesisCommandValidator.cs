using Ayllu.Application.Thesis.Commands;
using FluentValidation;

namespace Ayllu.Application.Thesis.Validators;

public sealed class CreateThesisCommandValidator
    : AbstractValidator<CreateThesisCommand>
{
    public CreateThesisCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithErrorCode("401")
            .WithMessage("User not authenticated");

        RuleFor(x => x.DialecticId)
            .NotEmpty()
            .WithErrorCode("400")
            .WithMessage("DialecticId is required");

        RuleFor(x => x.Request.Content)
            .NotEmpty()
            .WithErrorCode("400")
            .WithMessage("Thesis content is required")
            .MinimumLength(10)
            .WithErrorCode("422")
            .WithMessage("Thesis content must have at least 10 characters");
    }
}
