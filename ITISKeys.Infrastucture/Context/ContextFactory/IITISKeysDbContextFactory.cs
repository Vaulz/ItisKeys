namespace ITISKeys.Infrastructure.Context.ContextFactory
{
    public interface IITISKeysDbContextFactory
    {
        ITISKeysDbContext CreateDbContext(string connectionString);
    }
}
