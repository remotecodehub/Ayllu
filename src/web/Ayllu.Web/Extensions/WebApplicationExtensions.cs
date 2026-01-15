using Ayllu.Web.Application.Common.Abstractions.Data;
using Microsoft.Data.SqlClient;

namespace Ayllu.Web.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="WebApplication"/> class to facilitate database migration operations.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Attempts to migrate the database using the provided <see cref="WebApplication"/>.
    /// </summary>
    /// <param name="app">the <see cref="WebApplication"/> app instance to migrate</param>
    /// <returns></returns>
    public static async Task TryMigrateDbAsync(this WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILogger<WebApplication>>();
        try
        {
            using var scope = app.Services.CreateScope();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var rollback = config.GetValue<bool>("Database:RollbackBeforeMigrate");

            var migrator = scope.ServiceProvider.GetRequiredService<IApplicationDbMigrator>();
            await migrator.MigrateAsync(rollback);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error migrating database: {Message}", e.Message);
            throw new InvalidOperationException($"Error migrating database: {e.Message}", e);
        }
    }
    /// <summary>
    /// Determines whether the current application environment is set to "Test".
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to check the environment for. Cannot be null.</param>
    /// <returns>true if the application's environment name is "Test" (case-insensitive); otherwise, false.</returns>
    public static bool IsTestEnvironment(this WebApplication app) => app.Environment.EnvironmentName.Equals("Test", StringComparison.InvariantCultureIgnoreCase);
}
