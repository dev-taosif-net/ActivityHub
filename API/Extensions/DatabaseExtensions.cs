using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class DatabaseExtensions
{
    /// <summary>
    /// Brings the database up to date on startup: applies any pending EF Core migrations
    /// (creating the database if it does not exist), then seeds sample data when empty.
    /// Both steps are no-ops once the database is current and populated.
    /// </summary>
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            var context = services.GetRequiredService<AppDbContext>();

            var pending = (await context.Database.GetPendingMigrationsAsync()).ToList();
            if (pending.Count == 0)
            {
                logger.LogInformation("Database is up to date, no migrations to apply.");
            }
            else
            {
                logger.LogInformation(
                    "Applying {Count} pending migration(s): {Migrations}",
                    pending.Count,
                    string.Join(", ", pending));

                await context.Database.MigrateAsync();

                logger.LogInformation("Database migration completed.");
            }

            var seeded = await DbSeeder.SeedAsync(context);
            if (seeded > 0)
            {
                logger.LogInformation("Seeded {Count} sample activities.", seeded);
            }
            else
            {
                logger.LogInformation("Activities already present, skipping seed.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }
}
