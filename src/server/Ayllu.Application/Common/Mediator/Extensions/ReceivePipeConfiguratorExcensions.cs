
namespace Ayllu.Application.Common.Mediator.Extensions;

public static class ReceivePipeConfiguratorExcensions
{
    extension(ICommandReceivePipeConfigurator configurator)
    {
        public void UseFluentValidation()
            => configurator.AddPipeSpecification(new ValidationMiddlewareSpecification());

    }

    extension (IGlobalReceivePipeConfigurator configurator)
    {
        public void UseLogging()
            => configurator.AddPipeSpecification(new LoggingMiddlewareSpecification<IReceiveContext<IMessage>>());
    }
}
