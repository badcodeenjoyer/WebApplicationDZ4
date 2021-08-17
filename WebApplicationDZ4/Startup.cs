using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WebApplicationDZ4
{
    public class Startup
    {
        public Startup(IConfiguration configuration , ILoggerProvider loggerProvider , ILogger logger)
        {
            Configuration = configuration;
            Provider = loggerProvider;
            Logger = logger;
        }

        public IConfiguration Configuration { get; }
        public ILoggerProvider Provider { get; set; }
        public ILogger Logger { get; set; } 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerProvider, LoggerProvider>();
            services.AddSingleton<ILogger, MyLogger>();
            services.AddSingleton<ColorConsoleLoggerConfiguration>();
           

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplicationDZ4", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(Provider);
            var newlogger = loggerFactory.CreateLogger<MyLogger>();
          

            app.Run(async (context) =>
            {
                newlogger.LogInformation("Request", context.Request.Path);
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
