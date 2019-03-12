using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Playground.FunctionApp;

[assembly: WebJobsStartup(typeof(Startup), "Startup")]
namespace Playground.FunctionApp
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddSingleton<ISomeService, SomeService>();
            builder.Services.AddSingleton<IBindingProvider, SomeServiceBindingProvider>();
        }
    }
}
