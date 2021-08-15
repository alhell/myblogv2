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

namespace BlazorApp.Api
{
    public static class PostsFunction
    {
        [FunctionName("posts")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            var result = new List<Post>() {
            new Post(){ Date= new DateTime(2021,5,10), SubTitle="Problems look mighty small from 150 miles up",Title="Man must explore, and this is exploration at its greatest" } ,
            new Post(){ Date= new DateTime(2021,5,10), SubTitle="Is it really worth it?",Title="Is it really worth it?" }
            };
            return new OkObjectResult(result);
        }
    }
}
