using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TennisBookings.Web.Configuration.Custom;
using TennisBookings.Web.Data;

namespace TennisBookings.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var hostingEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
                var appLifetime = serviceProvider.GetRequiredService<IHostApplicationLifetime>();

                if (hostingEnvironment.IsDevelopment())
                {
                    var ctx = serviceProvider.GetRequiredService<TennisBookingDbContext>();
                    await ctx.Database.MigrateAsync(appLifetime.ApplicationStopping);

                    try
                    {
                        var userManager = serviceProvider.GetRequiredService<UserManager<TennisBookingsUser>>();
                        var roleManager = serviceProvider.GetRequiredService<RoleManager<TennisBookingsRole>>();

                        await SeedData.SeedUsersAndRoles(userManager, roleManager);
                    }
                    catch (Exception ex)
                    {
                        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("UserInitialisation");
                        logger.LogError(ex, "Failed to seed user data");
                    }
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
