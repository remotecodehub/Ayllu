namespace Ayllu.Application.Common.Abstractions.Data;

public interface IApplicationDbMigrator
{
    Task MigrateAsync(bool rollback);
}
