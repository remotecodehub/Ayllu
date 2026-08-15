using Ayllu.Application.Common.Abstractions.UnitOfWork;
using Ayllu.Infrastructure.Persistence.Data;
using Microsoft.Extensions.Logging;

namespace Ayllu.Infrastructure.Common.UnitOfWork;

public class UnitOfWork(ApplicationDbContext db, ILogger<UnitOfWork> logger) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
		try
		{
			logger.LogInformation("Saving changes to the database.");
            return await db.SaveChangesAsync(cancellationToken);
		}
		catch (Exception e)
		{
			logger.LogError(e, "{Message}", e.Message);
			throw;
		}
    }
}
