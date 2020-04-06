using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MeterReaderLib;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MeterReaderWeb
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
              webBuilder.ConfigureKestrel(opt =>
              {
                var config = opt.ApplicationServices.GetService<IConfiguration>();
                var cert = new X509Certificate2(config["Certificate:File"],
                                                config["Certificate:Password"]);
                
                opt.ConfigureHttpsDefaults(h =>
                {
                  h.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
                  h.CheckCertificateRevocation = false;
                  h.ServerCertificate = cert;
                });
              });
            });
  }
}
