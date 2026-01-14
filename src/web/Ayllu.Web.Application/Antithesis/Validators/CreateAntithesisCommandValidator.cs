using Ayllu.Web.Application.Antithesis.Commands;
using FluentValidation;

namespace Ayllu.Web.Application.Antithesis.Validators;

public sealed class CreateAntithesisCommandValidator
    : AbstractValidator<CreateAntithesisCommand>
{
    public CreateAntithesisCommandValidator()
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
            .WithMessage("Antithesis content is required")
            .MinimumLength(10)
            .WithErrorCode("422")
            .WithMessage("Antithesis content must have at least 10 characters");
    }
}