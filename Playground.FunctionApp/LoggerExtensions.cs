using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.FunctionApp.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Logz.Io;

namespace Playground.FunctionApp
{
    public static class LoggerExtensions
    {
        public static IServiceCollection AddLogzIo(this IServiceCollection services, IConfiguration configuration)
        {
            var logzIoOptions = services.ConfigureAsSingleton<LogzIoOptions>(configuration.GetSection("LogzIo"));

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.LogzIo(logzIoOptions.Token, logzIoOptions.LogType, new LogzioOptions { UseHttps = true, RestrictedToMinimumLevel = LogEventLevel.Debug })
                .CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

            return services;
        }
    }
}
