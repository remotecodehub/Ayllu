using Ayllu.Web.Application.Dialectics.Commands;
using FluentValidation;

namespace Ayllu.Web.Application.Dialectics.Validators;

public sealed class CreateDialecticCommandValidator
    : AbstractValidator<CreateDialecticCommand>
{
    public CreateDialecticCommandValidator()
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithErrorCode("401")
            .WithMessage("User not authenticated");

        RuleFor(x => x.Request.Title)
            .NotEmpty()
            .WithErrorCode("400")
            .WithMessage("Title is required")
            .MaximumLength(200)
            .WithErrorCode("400")
            .WithMessage("Title must have at most 200 characters");

        RuleFor(x => x.Request.Description)
            .NotEmpty()
            .WithErrorCode("400")
            .WithMessage("Description is required");
    }
}