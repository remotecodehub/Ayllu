namespace Ayllu.Application.Common.Mediator.Middlewares;
/// <summary>
/// Specification class for a validation middleware 
/// </summary>
public class ValidationMiddlewareSpecification : IPipeSpecification<IReceiveContext<ICommand>>
{
    /// <summary>
    /// This methods determines when the Pipeline has to be executed.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <remarks>When this method returns true, this pipeline executes for all commands</remarks>
    public bool ShouldExecute(IReceiveContext<ICommand> context, CancellationToken cancellationToken)
    {
        return true; 
    }

    /// <summary>
    /// Runs the fluent validation rules for the command, and throws exception on errors
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task BeforeExecute(IReceiveContext<ICommand> context, CancellationToken cancellationToken)
    {
        var message = context.Message;
        if (message == null) return;

        if (context.TryGetService<IServiceProvider>(out var serviceProvider))
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(message.GetType());

            if (serviceProvider.GetService(validatorType) is IValidator validator)
            {
                var validationContext = new ValidationContext<object>(message);
                var result = await validator.ValidateAsync(validationContext, cancellationToken);

                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
            }
        }
    }

    public Task Execute(IReceiveContext<ICommand> context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task AfterExecute(IReceiveContext<ICommand> context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Method called when an exception in this pipe is thrown
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <remarks>Here we only throw the exception, so that can be captured in a global exception handler </remarks>
    public Task OnException(Exception ex, IReceiveContext<ICommand> context)
    {
        throw ex;
    }
}
