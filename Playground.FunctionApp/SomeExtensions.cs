using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.FunctionApp.Configuration;

namespace Playground.FunctionApp
{
    public static class SomeServiceExtensions
    {
        public static void AddSomething(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAsSingleton<SomeOption>(configuration.GetSection("Something:SomeOption"));
            services.AddSingleton<ISomeService, SomeService>();
            services.AddSingleton<IBindingProvider, SomeServiceBindingProvider>();
        }
    }

    public class SomeOption
    {
        public string Text { get; set; }
        public int Number { get; set; }
    }
}
