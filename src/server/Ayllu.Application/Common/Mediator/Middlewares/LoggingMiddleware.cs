namespace Ayllu.Application.Common.Mediator.Middlewares;

public class LoggingMiddlewareSpecification<TContext> : IPipeSpecification<TContext>
    where TContext : IContext<IMessage>
{
    public bool ShouldExecute(TContext context, CancellationToken cancellationToken) => true;

    public Task BeforeExecute(TContext context, CancellationToken cancellationToken)
    {
        if (context.TryGetService<ILoggerFactory>(out var loggerFactory))
        {
            var logger = loggerFactory.CreateLogger(context.Message.GetType());
            logger.LogInformation("Processing message: {MessageType}", context.Message.GetType().Name);
        }

        return Task.CompletedTask;
    }

    public Task Execute(TContext context, CancellationToken cancellationToken) => Task.CompletedTask;

    public Task AfterExecute(TContext context, CancellationToken cancellationToken)
    {
        if (context.TryGetService<ILoggerFactory>(out var loggerFactory))
        {
            var logger = loggerFactory.CreateLogger(context.Message.GetType());
            logger.LogInformation("Completed processing: {MessageType}", context.Message.GetType().Name);
        }

        return Task.CompletedTask;
    }

    public Task OnException(Exception ex, TContext context)
    {
        if (context.TryGetService<ILoggerFactory>(out var loggerFactory))
        {
            var logger = loggerFactory.CreateLogger(context.Message.GetType());
            logger.LogError(ex, "Error processing message: {MessageType}", context.Message.GetType().Name);
        }

        throw ex;
    }
}