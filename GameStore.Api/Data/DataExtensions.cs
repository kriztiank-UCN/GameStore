// Purpose: Contains extension methods for the WebApplication class to migrate the database on startup.
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data
{
    public static class DataExtensions
    {
        public static void MigrateDb(this  WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
            dbContext.Database.Migrate();
        }

    }
}