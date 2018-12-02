using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Ingredient
{
    [BsonId]
    public ObjectId _id { get; set; }
    public string id { get; set; }
    public string name { get; set; }
    public decimal unitsPerPound { get; set; }
    public string unitOfMeasure { get; set; }
    public decimal unitCost { get; set; }
    public decimal costPerPound { get; set; }
}