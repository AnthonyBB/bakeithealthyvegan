using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Recipe
{
    [BsonId]
    public ObjectId _id { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int batchSize { get; set; }
    public double totalCost { get; set; }
}