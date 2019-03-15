using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Playground.FunctionApp;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Logz.Io;

[assembly: WebJobsStartup(typeof(Startup), "Startup")]
namespace Playground.FunctionApp
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSomething(config);

            builder.Services.AddLogzIo(config);

            Log.Information("Serilog initialize");
        }
    }

    [Extension("LogzIo")]
    public class LogzIoConfigProvider : IExtensionConfigProvider
    {
        private readonly IOptions<LogzIoOptions> _options;

        public LogzIoConfigProvider(IOptions<LogzIoOptions> options)
        {
            _options = options;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.LogzIo(_options.Value.Token, _options.Value.LogType, new LogzioOptions { UseHttps = true, RestrictedToMinimumLevel = LogEventLevel.Debug })
                .CreateLogger();
        }
    }

    public class LogzIoOptions
    {
        public string Token { get; set; }
        public string LogType { get; set; }
    }

}
