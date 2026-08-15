using Ayllu.Application.Common.Exceptions;
using FluentValidation;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace Ayllu.Application.Common.Behaviours;

/// <summary>
/// Mediator.Net pipeline specification that validates incoming messages with registered FluentValidation validators.
/// </summary>
public sealed class ValidationPipeSpecification(IServiceProvider serviceProvider)
    : IPipeSpecification<IReceiveContext<IMessage>>
{
    public bool ShouldExecute(IReceiveContext<IMessage> context, CancellationToken cancellationToken)
        => serviceProvider.GetService(typeof(IValidator<>).MakeGenericType(context.Message.GetType())) is not null;

    public async Task BeforeExecute(IReceiveContext<IMessage> context, CancellationToken cancellationToken)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(context.Message.GetType());
        if (serviceProvider.GetService(validatorType) is not IValidator validator)
        {
            return;
        }

        var validationContext = new ValidationContext<object>(context.Message);
        var result = await validator.ValidateAsync(validationContext, cancellationToken);
        var failure = result.Errors.FirstOrDefault();

        if (failure is not null)
        {
            throw new ApplicationValidationException(failure.ErrorCode, failure.ErrorMessage);
        }
    }

    public Task Execute(IReceiveContext<IMessage> context, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task AfterExecute(IReceiveContext<IMessage> context, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public void OnException(Exception ex, IReceiveContext<IMessage> context)
        => throw ex;
}
