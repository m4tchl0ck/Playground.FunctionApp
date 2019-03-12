using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
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

            log.LogInformation("Started doing something");

            someService.DoSomething();

            log.LogInformation("Finished doing something");

            return new OkResult();
        }
    }

    public class SomeService : ISomeService
    {
        public void DoSomething()
        {
            Thread.Sleep(5000);
        }
    }

    public interface ISomeService
    {
        void DoSomething();
    }
}
