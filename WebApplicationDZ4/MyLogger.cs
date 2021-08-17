using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Configuration;

namespace WebApplicationDZ4
{
    public class MyLogger : ILogger
    {
        private readonly string _loggername;
        private readonly Func<ColorConsoleLoggerConfiguration> _getCurrentConfig;
        private IConfiguration Configuration { get; set; }

        public MyLogger(string loggername, Func<ColorConsoleLoggerConfiguration> getCurrentConfig , IConfiguration configuration)
        {
            _getCurrentConfig = getCurrentConfig;
            _loggername = loggername;
            Configuration = configuration;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel) =>
            _getCurrentConfig().LogLevels.ContainsKey(logLevel);

        public void Log<TState>( LogLevel logLevel, EventId eventId,TState state, Exception exception,  Func<TState, Exception, string> formatter)
        {
       
            ColorConsoleLoggerConfiguration config = _getCurrentConfig();
            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                ConsoleColor originalColor = Console.ForegroundColor;
                Console.ForegroundColor = config.LogLevels[logLevel];
                Console.WriteLine($"ID:{eventId.Id} - {logLevel}");
                Console.ForegroundColor = originalColor;
                Console.WriteLine($"{_loggername} - {formatter(state, exception)}");
            }
        }
    }
    public class ColorConsoleLoggerConfiguration
    {
        public int EventId { get; set; }

        public Dictionary<LogLevel, ConsoleColor> LogLevels { get; set; } = new()
        {

        };
       
    }
}
