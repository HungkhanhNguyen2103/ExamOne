using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamOne.Entity
{
    public class ExamHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public int ExamId { get; set; }
        public string? Items { get; set; }
        public string? SelectedAnswers { get; set; }
        public int TotalCorrectAnswers { get; set; }
        public double ComplatedDuration { get; set; }
        public bool IsLoad { get; set; }
        public string? CreatedBy { get; set; }
        public string? BranchCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime MarkDate { get; set; }
    }
}
