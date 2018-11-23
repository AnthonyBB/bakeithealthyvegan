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
    public static class get_conversions
    {
        [FunctionName("get_conversions")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string connectionString = @"mongodb://devops-testing:bcbcl7so5CpPaMjVHgGiq5vxdahkZxMHpmcYO9K3lVkSxZB6NciUgZ4ZoOxKnkCGZgWQjS3zYAqjYX4t3p21Yw==@devops-testing.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
                MongoClientSettings settings = MongoClientSettings.FromUrl(
                  new MongoUrl(connectionString)
                );
                settings.SslSettings =
                  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var mongoClient = new MongoClient(settings);
                var database = mongoClient.GetDatabase("devops-testing");
                var conversionsQuery = database.GetCollection<StandardConversion>("standard-conversions").FindAsync(x => true);
                var conversions = await conversionsQuery;
                return (ActionResult)new OkObjectResult(conversions.ToList());
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unknown error occurred.");
                return new InternalServerErrorResult();
            }
        }
    }

    
    public class StandardConversion
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string cups { get; set; }
        public string tablespoons { get; set; }
        public string teaspoons { get; set; }
        public string fluidOunces { get; set; }
    }
}
