using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BlazorApp.Shared;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Cosmos;
using System.Configuration;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;

namespace BlazorApp.Api
{
    public static class BlogPostByRouteFunction
    {
        [FunctionName("blogpostbyroute")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "blogpostbyroute/{postroute}")] HttpRequest req,
            ILogger log, [CosmosDB(
                databaseName: "Blog",
                collectionName: "BlogPost",
                ConnectionStringSetting = "CosmosDBConnectionStringSetting",
                SqlQuery ="SELECT * FROM c where c.PostRoute={postroute}")] IEnumerable<BlogPost> blogPosts, string postroute)
        {
            var route = postroute;
            BlogPost post = blogPosts.FirstOrDefault();
            return new OkObjectResult(post);
        }
    }
}
