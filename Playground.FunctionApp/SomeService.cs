using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.Extensions.Logging;

namespace Playground.FunctionApp
{
    public static class UseSomeService
    {
        [FunctionName("UseSomeService")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
            ISomeService someService,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            someService.DoSomething();

            return new OkResult();
        }
    }

    public class SomeService : ISomeService
    {
        private readonly SomeOption _someOption;
        private readonly ILogger _log;

        public SomeService(SomeOption someOption, ILoggerFactory loggerFactory)
        {
            _someOption = someOption;
            _log = loggerFactory.CreateLogger("bla bla");
        }

        public void DoSomething()
        {
            _log.LogInformation("Started doing something");

            Thread.Sleep(5000);

            _log.LogInformation("Finished doing something");
        }

        public SomeOption GetSomeOption()
        {
            return _someOption;
        }
    }

    public interface ISomeService
    {
        void DoSomething();
        SomeOption GetSomeOption();
    }
}
