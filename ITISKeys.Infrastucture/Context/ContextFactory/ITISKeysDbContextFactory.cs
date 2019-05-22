using Microsoft.EntityFrameworkCore;

namespace ITISKeys.Infrastructure.Context.ContextFactory
{
    public class ITISKeysDbContextFactory : IITISKeysDbContextFactory
    {
        public ITISKeysDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ITISKeysDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new ITISKeysDbContext(optionsBuilder.Options);
        }
    }
}
