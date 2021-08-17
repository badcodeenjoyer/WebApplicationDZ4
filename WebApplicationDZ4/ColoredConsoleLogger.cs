using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace WebApplicationDZ4 
{
    public static class ColoredConsoleLogger
    {
        public static ILoggingBuilder AddColorLogging(
            this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions <ColorConsoleLoggerConfiguration, LoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddColorLogging(
            this ILoggingBuilder builder,
            Action<ColorConsoleLoggerConfiguration> configure)
        {
            builder.AddColorLogging();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}

