using Ayllu.Application.Dialectics.Commands;
using FluentValidation;

namespace Ayllu.Application.Dialectics.Validators;

public sealed class CloseDialecticCommandValidator
    : AbstractValidator<CloseDialecticCommand>
{
    public CloseDialecticCommandValidator()
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
    }
}