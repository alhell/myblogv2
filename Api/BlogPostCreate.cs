using System.IO;
using System.Net;
using System.Threading.Tasks;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace BlazorApp.Api
{
    public static class BlogPostCreate
    {
        [FunctionName("BlogPostCreate")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log, [CosmosDB(
                databaseName: "Blog",
                collectionName: "BlogPost",
                ConnectionStringSetting = "CosmosDBConnectionStringSetting")] IAsyncCollector<dynamic> documentsOut)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string postroute = req.Query["postroute"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var blogpost = JsonConvert.DeserializeObject<BlogPost>(requestBody);
            postroute = postroute ?? blogpost?.PostRoute;


            
                // Add a JSON document to the output container.
                await documentsOut.AddAsync(blogpost);
            

            string responseMessage = string.IsNullOrEmpty(postroute)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {postroute}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}

