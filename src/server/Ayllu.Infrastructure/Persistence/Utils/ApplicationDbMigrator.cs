using Ayllu.Application.Common.Abstractions.Data;
using Ayllu.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ayllu.Infrastructure.Persistence.Utils;


public class ApplicationDbMigrator(ApplicationDbContext context, ILogger<ApplicationDbMigrator> logger) : IApplicationDbMigrator
{
    public async Task MigrateAsync(bool rollback)
    {
        var applied = await context.Database.GetAppliedMigrationsAsync();
        var pending = await context.Database.GetPendingMigrationsAsync();

        if (rollback && applied.Any())
        {
            var last = applied.Last();
            logger.LogWarning("Revertendo migração: {Migration}", last);
            await context.Database.MigrateAsync(last);
        }

        if (pending.Any())
        {
            logger.LogInformation("Aplicando migrações pendentes...");
            await context.Database.MigrateAsync();
        }
    }

}
