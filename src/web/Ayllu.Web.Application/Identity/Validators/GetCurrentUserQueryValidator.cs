using Ayllu.Web.Application.Identity.Queries;
using FluentValidation;

namespace Ayllu.Web.Application.Identity.Validators;

public class GetCurrentUserQueryValidator : AbstractValidator<GetCurrentUserQuery>
{
    public GetCurrentUserQueryValidator()
    {
        RuleFor(q => q).NotNull();
        RuleFor(q => q.UserId).NotNull().WithErrorCode("400").WithMessage("O id do usuário não pode ser nulo!");
        RuleFor(q => q.UserId).NotEmpty().WithErrorCode("400").WithMessage("O id do usuário não pode ser vazio!");
        RuleFor(q => q.UserId).MinimumLength(36).WithErrorCode("422").WithMessage("O id do usuário possui comprimento mínimo de 36 caracteres!");
        RuleFor(q => q.UserId).MaximumLength(36).WithErrorCode("422").WithMessage("O id do usuário possui comprimento máximo de 36 caracteres!");
        RuleFor(q => q.UserId).Must(id => Guid.TryParse(id, out _)).WithErrorCode("422").WithMessage("O id do usuário deve ser uma string do tipo uuid!");
    }
}
