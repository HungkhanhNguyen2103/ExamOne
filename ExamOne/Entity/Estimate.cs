using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamOne.Entity
{
    public class Estimate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string? ExamId { get; set; }
        public string? CreatedBy { get; set; }
        public int EstimateCount { get; set; }
        public DateTime StartDate { get; set; }
    }
}
