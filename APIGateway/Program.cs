using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             .ConfigureAppConfiguration((host, config) =>
             {
                 config
                 .AddJsonFile("appsettings.json", true, true)
                 .AddJsonFile(System.IO.Path.Combine("Configuration", "ocelotConfiguration.json"))
                 .AddEnvironmentVariables();
             })
            .UseStartup<Startup>();

    }
}
