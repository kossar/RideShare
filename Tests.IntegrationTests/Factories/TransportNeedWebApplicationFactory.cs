using DAL.App.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tests.IntegrationTests.Data;

namespace Tests.IntegrationTests.Factories;

public class TransportNeedWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // find the dbcontext
            var descriptor = services
                .SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DbContext>)
                );
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<DbContext>(options =>
            {
                // do we need unique db?
                options.UseInMemoryDatabase(builder.GetSetting("test_database_name"));
            });
            
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AppDbContext>();

            db.Database.EnsureCreated();

            // data is already seeded
            if (db.Users.Any()) return;


            var applicationData = new ApplicationData(db);
            applicationData.InitializeData();
            db.SaveChanges();
            db.ChangeTracker.Clear();
        });
    }
}