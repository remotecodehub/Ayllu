using Ayllu.Application.Common.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ayllu.Application.Common.Behaviours;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failure = validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .FirstOrDefault();

        if (failure is not null)
        {
            throw new ApplicationValidationException(
                failure.ErrorCode,
                failure.ErrorMessage
            );
        }

        return await next(cancellationToken);
    }
}