using Ayllu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Ayllu.Composition;

public static class MigrationExtensions
{
    /// <summary>
    /// Aplica migrations pendentes e reverte migrations removidas do projeto.
    /// </summary>
    public static void ApplyMigrationsSafely(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var migrator = context.Database.GetService<IMigrator>();

        // 1. Migrations existentes no projeto
        var localMigrations = context.Database.GetMigrations().ToList();

        // 2. Migrations aplicadas no banco
        var appliedMigrations = context.Database.GetAppliedMigrations().ToList();

        // -------------------------------
        // 3. Reverte migrations removidas
        // -------------------------------
        var removedMigrations = appliedMigrations.Except(localMigrations).Reverse();
        foreach (var migration in removedMigrations)
        {
            Console.WriteLine($"Revertendo migration removida: {migration}");
            migrator.Migrate(GetPreviousMigration(localMigrations, migration));
        }

        // -------------------------------
        // 4. Aplica migrations pendentes
        // -------------------------------
        var pendingMigrations = localMigrations.Except(appliedMigrations);
        foreach (var migration in pendingMigrations)
        {
            Console.WriteLine($"Aplicando migration: {migration}");
            migrator.Migrate(migration);
        }
    }

    /// <summary>
    /// Retorna a migration imediatamente anterior no projeto para rollback.
    /// </summary>
    private static string GetPreviousMigration(List<string> localMigrations, string removedMigration)
    {
        var index = localMigrations.IndexOf(removedMigration);
        if (index <= 0)
            return "0"; // nenhuma migration aplicada
        return localMigrations[index - 1];
    }
}