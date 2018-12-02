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
    public static class delete_ingredient
    {
        [FunctionName("delete_ingredient")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request for 'delete_ingredient'.");

            //read json object from request body
            var content = req.Content;
            string jsonContent = await content.ReadAsStringAsync();
            DeleteIngredient model = JsonConvert.DeserializeObject<DeleteIngredient>(jsonContent);
            var name = model.name;

            string connectionString = @"mongodb://devops-testing:bcbcl7so5CpPaMjVHgGiq5vxdahkZxMHpmcYO9K3lVkSxZB6NciUgZ4ZoOxKnkCGZgWQjS3zYAqjYX4t3p21Yw==@devops-testing.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
                MongoClientSettings settings = MongoClientSettings.FromUrl(
                  new MongoUrl(connectionString)
                );

            settings.SslSettings =
                  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase("devops-testing");
            var ingredientQuery = await database.GetCollection<Ingredient>("ingredients")
                .FindAsync(x => x.name.ToLower() == name.ToLower());
            var ingredient = ingredientQuery.FirstOrDefault();

            if(ingredient != null)
            {
                var ingredientsQuery = database.GetCollection<Ingredient>("ingredients")
                    .DeleteOneAsync(x => x.name.ToLower() == name.ToLower());
                return (ActionResult)new OkResult();
            }
            else
            {
                return (ActionResult)new NotFoundResult();
            }
        }
    }
}
