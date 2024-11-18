using Microsoft.EntityFrameworkCore;

namespace GICEmployee.Infrastructure.Persistence
{
    public class DatabaseInitializer
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            // Ensure the database is created and apply migrations
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
        }
    }
}
