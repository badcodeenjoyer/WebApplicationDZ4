using System;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WebApplicationDZ4
{
    public class LoggerProvider : ILoggerProvider
    {
        private IConfiguration Configuration { get; set; }
        private ColorConsoleLoggerConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, MyLogger> _logger = new();

        public LoggerProvider(IOptions<ColorConsoleLoggerConfiguration> config , IConfiguration configuration)
        {
            Configuration = configuration;
            _currentConfig = config.Value;    
        }

        public ILogger CreateLogger(string categoryName)
        {
          return  _logger.GetOrAdd(categoryName, new MyLogger(categoryName, GetCurrentConfig, Configuration));
         
        }
        private ColorConsoleLoggerConfiguration GetCurrentConfig() => _currentConfig;

        public void Dispose()
        {
            
        }
    }
}
