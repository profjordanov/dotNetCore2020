using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Chain_of_Responsibility
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) => 
            {
                Console.WriteLine("Handling #1");
                await next.Invoke();
                Console.WriteLine("Done #1");
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Handling #2");
                await next.Invoke();
                Console.WriteLine("Done #2");
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Handling #3");
                await next.Invoke();
                Console.WriteLine("Done #3");
            });
        }
    }
}
