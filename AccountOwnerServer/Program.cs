using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountOwnerServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        // CreateDefaultBuilder(args) – ASP.NET Core uses this to configure 
        //the app configuration, logging, and dependency injection container

        // ConfigureWebHostDefaults – This adds everything else required by a typical
        // ASP.NET Core application (kestrel configuration, and using the Startup class, middleware pipeline…)
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {//statup is use for ConfigureServices
                    webBuilder.UseStartup<Startup>();
                });
    }
}
