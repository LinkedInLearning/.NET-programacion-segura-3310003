using Microsoft.EntityFrameworkCore;

namespace Wpm.Web.Dal;

public static class WpmDbContextExtensions
{
    public static void AddSqliteWpmDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WpmDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("Local"));
        });
    }
    public static void EnsureWpmDbIsCreated(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetService<WpmDbContext>()!;
        db.Database.EnsureCreated();
        db.Dispose();
    }
}