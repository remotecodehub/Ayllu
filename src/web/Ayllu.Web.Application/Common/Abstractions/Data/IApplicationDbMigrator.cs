namespace Ayllu.Web.Application.Common.Abstractions.Data;

public interface IApplicationDbMigrator
{
    Task MigrateAsync(bool rollback);
}
