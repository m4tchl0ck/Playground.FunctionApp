using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Playground.FunctionApp
{
    public static class LogWithSerilog
    {
        [FunctionName(nameof(LogWithSerilog))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var obj = JsonConvert.DeserializeObject<SomeClass>(requestBody);

            log.LogInformation("{@obj}", obj);

            return new OkResult();
        }

        public class SomeClass
        {
            [JsonProperty("text")]
            public string Text { get; set; }
            [JsonProperty("number")]
            public int Number { get; set; }
        }
    }
}
