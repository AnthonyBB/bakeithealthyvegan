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
    public static class put_ingredient
    {
        [FunctionName("put_ingredient")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequestMessage req,
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

                //read json object from request body
                var content = req.Content;
                string jsonContent = await content.ReadAsStringAsync();
                log.LogInformation(jsonContent);
                Ingredient model = JsonConvert.DeserializeObject<Ingredient>(jsonContent);

                var cursor = await database.GetCollection<Ingredient>("ingredients")
                    .FindAsync(x => x.name.ToLower() == model.name.ToLower());

                var ingredient = cursor.SingleOrDefault();

                if(ingredient != null)
                {
                    ingredient.name = model.name;
                    ingredient.unitOfMeasure = model.unitOfMeasure;
                    ingredient.unitsPerPound = model.unitsPerPound;
                    ingredient.unitCost = model.unitCost;
                    ingredient.costPerPound = model.costPerPound;
                    await database.GetCollection<Ingredient>("ingredients")
                        .ReplaceOneAsync(x => x.name.ToLower() == model.name.ToLower(), ingredient);
                }
                else
                {
                    await database.GetCollection<Ingredient>("ingredients")
                        .InsertOneAsync(model);
                }
                log.LogInformation(JsonConvert.SerializeObject(model));
                return (ActionResult)new OkObjectResult(model);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unknown error occurred.");
                return new InternalServerErrorResult();
            }
        }
    }
}
