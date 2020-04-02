using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TennisBookings.Web.Core.DependencyInjection;
using TennisBookings.Web.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using TennisBookings.Web.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using TennisBookings.ResultsProcessing;
using Amazon.S3;
using TennisBookings.Web.BackgroundServices;
using TennisBookings.Web.Core;

namespace TennisBookings.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.PostConfigure<HostOptions>(option =>
            {
                option.ShutdownTimeout = TimeSpan.FromSeconds(60); // allow up to 60 seconds to complete any in-progress results processing.
            });
            
            services.AddDbContext<TennisBookingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<HomePageConfiguration>(Configuration.GetSection("Features:HomePage"));

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<HomePageConfiguration>, HomePageConfigurationValidation>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<ExternalServicesConfig>, ExternalServicesConfigurationValidation>());
            services.AddHostedService<ValidateOptionsService>();

            services.Configure<GreetingConfiguration>(Configuration.GetSection("Features:Greeting"));
            services.Configure<WeatherForecastingConfiguration>(Configuration.GetSection("Features:WeatherForecasting"));
            services.Configure<ExternalServicesConfig>(ExternalServicesConfig.WeatherApi, Configuration.GetSection("ExternalServices:WeatherApi"));
            
            services.Configure<ScoreProcesingConfiguration>(Configuration.GetSection("ScoreProcessing"));

            services.Configure<ContentConfiguration>(Configuration.GetSection("Content"));
            services.AddSingleton<IContentConfiguration>(sp =>
                sp.GetRequiredService<IOptions<ContentConfiguration>>().Value);

            services
                .AddAppConfiguration(Configuration)
                .AddBookingServices()
                .AddBookingRules()
                .AddCourtUnavailability()
                .AddMembershipServices()
                .AddStaffServices()
                .AddCourtServices()
                .AddWeatherForecasting(Configuration)
                .AddNotifications()
                .AddGreetings()
                .AddCaching()
                .AddTimeServices()
                .AddAuditing()
                .AddContentServices();

            services.AddTennisPlayerApiClient(options => options.BaseAddress = Configuration.GetSection("ExternalServices:TennisPlayersApi")["Url"]);
            services.AddStatisticsApiClient(options => options.BaseAddress = Configuration.GetSection("ExternalServices:StatisticsApi")["Url"]);
            services.AddResultProcessing();

            services.AddSingleton<FileProcessingChannel>();

            if (Configuration.IsWeatherForecastEnabled())
                services.AddHostedService<WeatherCacheService>();

            services.AddHostedService<FileProcessingService>();

            services.AddControllersWithViews();
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizePage("/FindAvailableCourts");
                options.Conventions.AuthorizePage("/BookCourt");
                options.Conventions.AuthorizePage("/Bookings");
            });

            services.AddIdentity<TennisBookingsUser, TennisBookingsRole>()
                .AddEntityFrameworkStores<TennisBookingDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddAWSService<IAmazonS3>();

            var useLocalStack = Configuration.GetValue<bool>("UseLocalStack");

            if (WebHostEnvironment.IsDevelopment() && useLocalStack)
            {
                services.AddSingleton<IAmazonS3>(sp =>
                {
                    var s3Client = new AmazonS3Client(new AmazonS3Config
                    {
                        ServiceURL = "http://localhost:4572",
                        ForcePathStyle = true,
                    });

                    return s3Client;
                });
            }          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
