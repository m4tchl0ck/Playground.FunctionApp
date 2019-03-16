using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Playground.FunctionApp.Configuration;

namespace Playground.FunctionApp
{
    public static class GetSomeOptionValue
    {
        [FunctionName(nameof(GetSomeOptionValue))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")]
            HttpRequest req, ILogger log, ISomeService someService)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            return new OkObjectResult(someService.GetSomeOption());
        }
    }
}
