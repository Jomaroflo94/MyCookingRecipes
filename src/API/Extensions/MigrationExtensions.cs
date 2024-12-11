using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using AppWriteDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<AppWriteDbContext>();

        dbContext.Database.EnsureDeleted();//TODO: SOLO DESARROLLO
        dbContext.Database.Migrate();
    }
}
