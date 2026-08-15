using Ayllu.Application.Synthesis.Commands;
using FluentValidation;

namespace Ayllu.Application.Synthesis.Validators;

public sealed class CreateSynthesisCommandValidator
    : AbstractValidator<CreateSynthesisCommand>
{
    public CreateSynthesisCommandValidator()
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
            .WithMessage("Synthesis content is required")
            .MinimumLength(15)
            .WithErrorCode("422")
            .WithMessage("Synthesis content must have at least 15 characters");
    }
}