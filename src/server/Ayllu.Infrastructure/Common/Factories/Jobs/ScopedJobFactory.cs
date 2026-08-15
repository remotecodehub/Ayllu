using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Ayllu.Infrastructure.Common.Factories.Jobs;

public class ScopedJobFactory(IServiceProvider serviceProvider) : IJobFactory
{
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var scope = serviceProvider.CreateScope();
        var job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        return job!;
    }

    public void ReturnJob(IJob job) { /* opcional */ }
}
