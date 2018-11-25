using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using MongoDB.Driver;
using System.Security.Authentication;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevOps.Testing
{
    public static class get_recipe
    {
        [FunctionName("get_recipe")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string name = req.Query["name"];
                string connectionString = @"mongodb://devops-testing:bcbcl7so5CpPaMjVHgGiq5vxdahkZxMHpmcYO9K3lVkSxZB6NciUgZ4ZoOxKnkCGZgWQjS3zYAqjYX4t3p21Yw==@devops-testing.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
                MongoClientSettings settings = MongoClientSettings.FromUrl(
                  new MongoUrl(connectionString)
                );
                settings.SslSettings =
                  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                var database = mongoClient.GetDatabase("devops-testing");
                var recipesQuery = await database.GetCollection<Recipe>("recipes").FindAsync(x => x.name.ToLower() == name.ToLower());
                var recipe = recipesQuery.SingleOrDefault();

                if(recipe == null)
                {
                    return (ActionResult)new NotFoundResult();
                }
                
                return (ActionResult)new OkObjectResult(recipe);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unknown error occurred.");
                return new InternalServerErrorResult();
            }
        }
    }
}
