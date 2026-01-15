using Ayllu.Web.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ayllu.Web.Infrastructure.HealthChecks.Database;


public sealed class DatabaseUsageCheck(ApplicationDbContext db) : IHealthCheck
{
    internal sealed record DatabaseDiskUsageResult(int Size, int Used);

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await db.Database
            .SqlQueryRaw<DatabaseDiskUsageResult>(
                @"SELECT SUM(size) * 8 * 1024 AS size,
                         SUM(FILEPROPERTY(name, 'SpaceUsed')) * 8 * 1024 AS used
                  FROM sys.master_files
                  WHERE database_id = DB_ID()")
            .ToListAsync(cancellationToken);

            var usage = result.SingleOrDefault() ?? throw new Exception("Cannot calculate database usage on disk");
            var free = usage.Size - usage.Used;
            var data = new Dictionary<string, object>
            {
                {
                    "DatabaseSize",
                    new
                    {
                        TotalSize = usage.Size,
                        UsedSize = usage.Used,
                        FreeSize = free
                    }
                }
            };

            if (free > 2_000)
                return HealthCheckResult.Healthy("Database has some considerable free space", data);

            if (free < 500)
                return HealthCheckResult.Degraded("Database free space is too low", null, data);

            return HealthCheckResult.Unhealthy("Cannot calculate database usage on disk");
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy("Cannot calculate database usage on disk", e);
        }
    }
}
