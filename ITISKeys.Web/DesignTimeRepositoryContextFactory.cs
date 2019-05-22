using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ITISKeys.Infrastructure.Context;
using ITISKeys.Infrastructure.Context.ContextFactory;

namespace ITISKeys.Web
{
    public class DesignTimeRepositoryContextFactory : IDesignTimeDbContextFactory<ITISKeysDbContext>
    {
        public ITISKeysDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            var repositoryFactory = new ITISKeysDbContextFactory();
            return repositoryFactory.CreateDbContext(connectionString);
        }
    }
}
