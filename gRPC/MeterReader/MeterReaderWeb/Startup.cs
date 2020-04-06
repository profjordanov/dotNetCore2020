using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MeterReaderLib;
using MeterReaderWeb.Data;
using MeterReaderWeb.Services;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeterReaderWeb
{
  public class Startup
  {
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
      _config = config;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<JwtTokenValidationService>();
      services.AddAuthentication()
        .AddJwtBearer(cfg =>
        {
          cfg.TokenValidationParameters = new MeterReaderTokenValidationParameters(_config);
        })
        .AddCertificate(opt =>
        {
          opt.AllowedCertificateTypes = CertificateTypes.SelfSigned;
          opt.RevocationMode = X509RevocationMode.NoCheck; // Self-Signed Certs (Development)
          opt.Events = new CertificateAuthenticationEvents()
          {
            OnCertificateValidated = ctx =>
            {
              // Write additional Validation  
              ctx.Success();
              return Task.CompletedTask;
            }
          };
        });

      services.AddDbContext<ReadingContext>(options =>
          options.UseSqlServer(
              _config.GetConnectionString("DefaultConnection")));

      services.AddDefaultIdentity<IdentityUser>()
        .AddEntityFrameworkStores<ReadingContext>();

      services.AddControllersWithViews();
      services.AddRazorPages();

      services.AddScoped<IReadingRepository, ReadingRepository>();

      services.AddGrpc(opt =>
      {
        opt.EnableDetailedErrors = true;
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGrpcService<MeterService>();

        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
      });
    }
  }
}
