using Application.Settings;
using Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Concrete;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static async Task AddPersistenceService(this IServiceCollection collection)
        {
            collection.AddScoped<IMovieService, MovieService>();
            using var provider = collection.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();
            var tenantSettings = configuration.GetSection("TenantSettings").Get<TenantSettings>();

            var defaultConnectionString = tenantSettings.Defaults?.ConnectionString;
            var defaultDbProvider = tenantSettings.Defaults?.DBProvider;
            if (defaultDbProvider.ToLower() == "mssql")
                collection.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(e => e.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            using IServiceScope scope = collection.BuildServiceProvider().CreateScope();

            foreach (var tenant in tenantSettings.Tenants)
            {
                string connectionString = tenant.ConnectionString switch
                {
                    null => defaultConnectionString,
                    not null => tenant.ConnectionString
                };
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.SetConnectionString(connectionString);
                if (dbContext.Database.GetMigrations().Count() > 0)
                    await dbContext.Database.MigrateAsync();
            }
        }
    }
}
