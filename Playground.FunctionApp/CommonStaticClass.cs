using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Playground.FunctionApp
{
	public static class CommonStaticClass
	{
		public static string SomeValue { get; set; }
	}

    public static class GetCommonStaticClassValue
    {
        [FunctionName(nameof(GetCommonStaticClassValue))]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            return new OkObjectResult(CommonStaticClass.SomeValue);
        }
    }

    public static class SetCommonStaticClassValue
    {
        [FunctionName(nameof(SetCommonStaticClassValue))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            CommonStaticClass.SomeValue = await new StreamReader(req.Body).ReadToEndAsync();

            return new OkResult();
        }
    }
}
