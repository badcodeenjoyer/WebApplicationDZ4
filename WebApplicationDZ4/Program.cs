using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplicationDZ4
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            var logger = host.Services.GetRequiredService<ILogger<MyLogger>>();

            
            logger.LogError(1, "Test for Error");
            logger.LogWarning(2, "Test for Warning");

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            
         .ConfigureLogging(builder =>
                 builder.ClearProviders()
                     .AddColorLogging(configuration =>
                     {
                         configuration.LogLevels.Add( LogLevel.Warning, ConsoleColor.Blue);
                         configuration.LogLevels.Add( LogLevel.Error, ConsoleColor.Cyan);
                         configuration.LogLevels.Add( LogLevel.Information, ConsoleColor.Yellow);
                     }));
        

         }
    }
